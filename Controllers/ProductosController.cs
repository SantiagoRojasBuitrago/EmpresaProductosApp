using Microsoft.AspNetCore.Mvc;

using System.Linq;

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
        ViewBag.Empresas = _context.Empresas.ToList(); // Para mostrar las empresas en un dropdown
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Empresas = _context.Empresas.ToList(); // En caso de error, recargar la lista
        return View(producto);
    }

    public IActionResult Index()
    {
        var productos = _context.Productos.ToList();
        return View(productos);
    }
}
