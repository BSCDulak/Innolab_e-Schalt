# todo umstellen, damit es von package.json installiert
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
ENV DOTNET_GENERATE_ASPNET_CERTIFICATE=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY eSchalt/package.json ./
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs
RUN npm install

WORKDIR /src
COPY ["eSchalt/eSchalt.csproj", "eSchalt/"]
RUN dotnet restore "./eSchalt/./eSchalt.csproj"
COPY . .
WORKDIR "/src/eSchalt"
RUN dotnet build "./eSchalt.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./eSchalt.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "eSchalt.dll"]