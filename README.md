**Proyecto: EmpresaProductosApp**

**Descripción:**
EmpresaProductosApp es una aplicación web diseñada para gestionar productos y usuarios, desarrollada con ASP.NET Core y PostgreSQL. Proporciona una interfaz fácil de usar para la administración de datos e inventario.

**Tecnologías:**
- ASP.NET Core 8.0
- PostgreSQL
- Docker

**Requisitos:**
- .NET 8.0 SDK
- PostgreSQL
- Docker

**Instalación:**
1. Clonar el repositorio:
   - Ejecutar el comando: `git clone [https://github.com/tu-usuario/EmpresaProductosApp.git](https://github.com/SantiagoRojasBuitrago/EmpresaProductosApp)`
   - Navegar a la carpeta del proyecto: `cd EmpresaProductosApp`
   
2. Restaurar las dependencias:
   - Ejecutar el comando: `dotnet restore`
   
3. Configurar la cadena de conexión a PostgreSQL en el archivo `appsettings.json`:
   - Editar el archivo para incluir los detalles de la base de datos.

4. Aplicar las migraciones de la base de datos:
   - Ejecutar el comando: `dotnet ef database update`

**Ejecución:**
Para ejecutar la aplicación localmente, utilizar el comando:
- `dotnet run`
La aplicación estará disponible en `http://localhost:5248`.

**Uso:**
- Acceder a la aplicación en el navegador.
- Rutas disponibles:
  - Login: `/Usuarios/Login`
  - Home: `/Home`
  - Privacidad: `/Privacy`


