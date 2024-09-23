using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
[Authorize]
public class ProductosController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Crear()
    {
        ViewBag.Empresas = _context.Empresas.ToList(); // Obtener las empresas para el dropdown
        var productos = _context.Productos.ToList(); // Obtener la lista de productos existentes
        return View(productos);
    }

    [HttpPost]
    public IActionResult Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return RedirectToAction("Crear"); // Volver a cargar la vista de crear
        }

        ViewBag.Empresas = _context.Empresas.ToList(); // En caso de error, recargar la lista de empresas
        var productos = _context.Productos.ToList(); // Recargar la lista de productos en caso de error
        return View(productos);
    }

    public IActionResult Index()
    {
        var productos = _context.Productos.ToList();
        return View(productos);
    }
}
