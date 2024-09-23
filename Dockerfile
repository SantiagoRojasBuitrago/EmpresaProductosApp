# Usar la imagen del SDK de .NET 8.0 para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo csproj y restaurar las dependencias
COPY EmpresaProductosApp.csproj ./
RUN dotnet restore

# Copiar todo el resto del código y construir
COPY . .
RUN dotnet build -c Release -o /app/build

# Usar la imagen de ASP.NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/build .

# Exponer el puerto
EXPOSE 80
ENTRYPOINT ["dotnet", "EmpresaProductosApp.dll"]
