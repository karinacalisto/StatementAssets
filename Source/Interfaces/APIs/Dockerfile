FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY["nuget.config", "."]
COPY ["Source/Interfaces/APIs/StatementAssets.csproj", "Source/Interfaces/APIs/"]
COPY ["Source/Core/Ingestion/Statement/Statement.csproj", "Source/Core/Ingestion/Statement/"]
COPY ["Source/Core/Shared/Shared.csproj", "Source/Core/Shared/"]
COPY ["Source/Infrastructure/Data/Data.Statement/Statement.Data/Statement.Data.csproj", "Source/Infrastructure/Data/Data.Statement/Statement.Data/"]
COPY ["Source/Infrastructure/Data/Data.Shared/Data.Shared/Data.Shared.csproj", "Source/Infrastructure/Data/Data.Shared/Data.Shared/"]
RUN dotnet restore "./Source/Interfaces/APIs/StatementAssets.csproj"
COPY . .
WORKDIR "/src/Source/Interfaces/APIs"
RUN dotnet build "./StatementAssets.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StatementAssets.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StatementAssets.dll"]