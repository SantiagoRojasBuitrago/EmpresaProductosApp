#!/bin/bash

# Esperar a que PostgreSQL esté disponible
while ! nc -z $DB_HOST $DB_PORT; do
  echo "Esperando a que PostgreSQL esté disponible..."
  sleep 2
done

# Aplicar migraciones de Entity Framework
echo "Aplicando migraciones de Entity Framework..."
dotnet ef database update --project /src/EmpresaProductosApp.csproj --no-build --connection "$CONNECTION_STRING"

# Iniciar la aplicación
exec dotnet EmpresaProductosApp.dll
