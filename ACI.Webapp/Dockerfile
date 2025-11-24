FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ACI.Webapp/ACI.Webapp.csproj", "ACI.Webapp/"]
RUN dotnet restore "ACI.Webapp/ACI.Webapp.csproj"

COPY ACI.Webapp/package*.json ACI.Webapp/
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm@latest
WORKDIR /src/BookSnaps.Webapp
RUN npm i

COPY . .
WORKDIR "/src/ACI.Webapp"
RUN dotnet build "./ACI.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN npm run build 
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ACI.Webapp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACI.Webapp.dll"]
