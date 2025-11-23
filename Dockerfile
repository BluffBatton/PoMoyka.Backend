# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["PoMoyka.Backend.API/PoMoyka.Backend.API.csproj", "PoMoyka.Backend.API/"]
COPY ["PoMoyka.Backend.Application/PoMoyka.Backend.Application.csproj", "PoMoyka.Backend.Application/"]
COPY ["PoMoyka.Backend.Contracts/PoMoyka.Backend.Contracts.csproj", "PoMoyka.Backend.Contracts/"]
COPY ["PoMoyka.Backend.Domain/PoMoyka.Backend.Domain.csproj", "PoMoyka.Backend.Domain/"]
COPY ["PoMoyka.Backend.Infrastructure/PoMoyka.Backend.Infrastructure.csproj", "PoMoyka.Backend.Infrastructure/"]
RUN dotnet restore "PoMoyka.Backend.API/PoMoyka.Backend.API.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/PoMoyka.Backend.API"
RUN dotnet publish "PoMoyka.Backend.API.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "PoMoyka.Backend.API.dll"]

