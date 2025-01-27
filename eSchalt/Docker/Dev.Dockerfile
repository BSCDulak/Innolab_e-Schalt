FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug

WORKDIR /src
RUN apt-get update && apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs
COPY eSchalt/package.json eSchalt/package-lock.json* ./eSchalt/
WORKDIR /src/eSchalt
RUN npm install
WORKDIR /src
COPY eSchalt/eSchalt.csproj eSchalt/
RUN dotnet restore eSchalt/eSchalt.csproj
COPY . .

WORKDIR /src/eSchalt
RUN npm run sass
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
RUN npm install

ENV ASPNETCORE_URLS=http://*:5000
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1
ENV DOTNET_WATCH_SUPPRESS_MSBUILD_INCREMENTALISM=1
ENV DOTNET_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "eSchalt.dll"]
