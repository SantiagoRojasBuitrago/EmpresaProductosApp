using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
    public IActionResult Login(string correo, string contrasena)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == correo);
        if (usuario != null && BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
        {
            // Autenticación exitosa
            return RedirectToAction("Index", "Home");
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
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena); // Encriptar la contraseña
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(usuario);
    }
}
