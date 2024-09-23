using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión para PostgreSQL.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Registrar DinkToPdf
builder.Services.AddSingleton<ITools, PdfTools>();
builder.Services.AddSingleton<IConverter, SynchronizedConverter>();

// Configurar autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuarios/Login"; // Ruta para iniciar sesión
        options.LogoutPath = "/Usuarios/CerrarSesion"; // Ruta para cerrar sesión
        options.AccessDeniedPath = "/Usuarios/AccesoDenegado"; // Ruta para acceso denegado
    });

var app = builder.Build();

// Configurar el pipeline de manejo de errores y el entorno.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Asegúrate de añadir esto
app.UseAuthorization();

// Middleware para redirigir a /Usuarios/Login
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Usuarios/Login");
        return; // Detener la ejecución del middleware
    }
    await next(); // Continuar con el siguiente middleware
});

// Configurar las rutas de los controladores.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
