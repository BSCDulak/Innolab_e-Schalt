FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
RUN apt-get update && apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs
RUN npm install -g sass
COPY eSchalt/eSchalt.csproj eSchalt/
RUN dotnet restore eSchalt/eSchalt.csproj
COPY . .

WORKDIR /src/eSchalt
RUN sass ./Frontend/Scss:./wwwroot/css
RUN dotnet build "eSchalt.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eSchalt.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER root
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get update && apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs
RUN npm install -g sass
RUN node -v && npm -v

ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "eSchalt.dll"]
