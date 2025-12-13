# 1. Build Mərhələsi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proyekt fayllarını kopyalayırıq (Restore üçün)
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["Data.MSSQL/Data.MSSQL.csproj", "Data.MSSQL/"]
COPY ["Domain/Domain.csproj", "Domain/"]

# Paketləri yükləyirik
RUN dotnet restore "WebAPI/WebAPI.csproj"

# Bütün kodları kopyalayırıq
COPY . .

# Build və Publish edirik
WORKDIR "/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# 2. Run Mərhələsi (Final Image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Portları açırıq (Kestrel daxildə 8080-də işləyir)
EXPOSE 8080

# App-i işə salırıq
ENTRYPOINT ["dotnet", "WebAPI.dll"]