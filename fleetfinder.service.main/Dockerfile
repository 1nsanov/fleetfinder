#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["fleetfinder.service.main/fleetfinder.service.main.csproj", "fleetfinder.service.main/"]
COPY ["fleetfinder.service.main.application/fleetfinder.service.main.application.csproj", "fleetfinder.service.main.application/"]
COPY ["fleetfinder.service.main.infrastructure/fleetfinder.service.main.infrastructure.csproj", "fleetfinder.service.main.infrastructure/"]
COPY ["fleetfinder.service.main.domain/fleetfinder.service.main.domain.csproj", "fleetfinder.service.main.domain/"]
RUN dotnet restore "fleetfinder.service.main/fleetfinder.service.main.csproj"
COPY . .
WORKDIR "/src/fleetfinder.service.main"
RUN dotnet build "fleetfinder.service.main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fleetfinder.service.main.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fleetfinder.service.main.dll"]