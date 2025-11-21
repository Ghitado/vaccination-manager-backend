FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/VaccinationManager.Api/VaccinationManager.Api.csproj", "src/VaccinationManager.Api/"]
COPY ["src/VaccinationManager.Application/VaccinationManager.Application.csproj", "src/VaccinationManager.Application/"]
COPY ["src/VaccinationManager.Domain/VaccinationManager.Domain.csproj", "src/VaccinationManager.Domain/"]
COPY ["src/VaccinationManager.Infrastructure/VaccinationManager.Infrastructure.csproj", "src/VaccinationManager.Infrastructure/"]
RUN dotnet restore "./src/VaccinationManager.Api/VaccinationManager.Api.csproj"
COPY . .
WORKDIR "/src/src/VaccinationManager.Api"
RUN dotnet build "./VaccinationManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VaccinationManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VaccinationManager.Api.dll"]