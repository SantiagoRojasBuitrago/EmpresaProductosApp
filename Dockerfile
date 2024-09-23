# Usar la imagen del SDK de .NET para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EmpresaProductosApp.csproj", "./"]
RUN dotnet restore "EmpresaProductosApp.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "EmpresaProductosApp.csproj" -c Release -o /app/build

# Usar la imagen de ASP.NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=build /app/build .

# Exponer el puerto
EXPOSE 80
ENTRYPOINT ["dotnet", "EmpresaProductosApp.dll"]
