using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Claims = System.Security.Claims.ClaimTypes; // Usar un alias


public class UsuariosController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsuariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string correo, string contrasena)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == correo);
        if (usuario != null && BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
        {
            // Crear los claims para la autenticación
            var claims = new List<Claim>
            {
                new Claim(Claims.NameIdentifier, usuario.Correo.ToString()), // Usar el alias
                new Claim(Claims.Name, usuario.Correo) // Usar el alias
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Persistir la sesión
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Tiempo de expiración
            };

            // Iniciar sesión
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Autenticación exitosa
            return RedirectToAction("Index", "Dashboard");
        }

        // Fallo en la autenticación
        ModelState.AddModelError("", "Correo o contraseña incorrecta");
        return View();
    }

    [HttpGet]
    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registrar(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> CerrarSesion()
    {
        // Cerrar sesión
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Redirigir a Shared/Index
        return RedirectToAction("Login", "Usuarios");
    }
}
