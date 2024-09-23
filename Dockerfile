# Usa la imagen base de .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmpresaProductosApp/EmpresaProductosApp.csproj", "EmpresaProductosApp/"]
RUN dotnet restore "EmpresaProductosApp/EmpresaProductosApp.csproj"
COPY . .
WORKDIR "/src/EmpresaProductosApp"
RUN dotnet build "EmpresaProductosApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmpresaProductosApp.csproj" -c Release -o /app/publish

# Usa la imagen base para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmpresaProductosApp.dll"]
