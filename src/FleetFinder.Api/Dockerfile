#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0.10 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0.302 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "./"]
COPY ["global.json", "./"]
COPY ["src/FleetFinder.Api/FleetFinder.Api.csproj", "src/FleetFinder.Api/"]
COPY ["src/FleetFinder.Application/FleetFinder.Application.csproj", "src/FleetFinder.Application/"]
COPY ["src/FleetFinder.Infrastructure/FleetFinder.Infrastructure.csproj", "src/FleetFinder.Infrastructure/"]
COPY ["src/FleetFinder.Domain/FleetFinder.Domain.csproj", "src/FleetFinder.Domain/"]
RUN dotnet restore "src/FleetFinder.Api/FleetFinder.Api.csproj"
COPY . .
WORKDIR "/src/src/FleetFinder.Api"
RUN dotnet build "FleetFinder.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetFinder.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetFinder.Api.dll"]
