# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmpresaProductosApp/EmpresaProductosApp.csproj", "EmpresaProductosApp/"]
RUN dotnet restore "EmpresaProductosApp/EmpresaProductosApp.csproj"
COPY . .
WORKDIR "/src/EmpresaProductosApp"
RUN dotnet build "EmpresaProductosApp.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "EmpresaProductosApp.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmpresaProductosApp.dll"]
