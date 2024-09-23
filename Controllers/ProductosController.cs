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
        ViewBag.Empresas = _context.Empresas.ToList(); 
        var productos = _context.Productos.ToList(); 
        return View(productos);
    }

    [HttpPost]
    public IActionResult Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return RedirectToAction("Crear");
        }

        ViewBag.Empresas = _context.Empresas.ToList(); 
        var productos = _context.Productos.ToList(); 
        return View(productos);
    }

    public IActionResult Index()
    {
        var productos = _context.Productos.ToList();
        return View(productos);
    }

    [HttpGet]
    public IActionResult Editar(string id)
    {
        var producto = _context.Productos.Find(id);
        if (producto == null)
        {
            return NotFound();
        }

        ViewBag.Empresas = _context.Empresas.ToList(); 
        return View(producto);
    }

    [HttpPost]
    public IActionResult Editar(Producto productoActualizado)
    {
        if (ModelState.IsValid)
        {
            _context.Productos.Update(productoActualizado);
            _context.SaveChanges();
            return RedirectToAction("Crear"); 
        }

        ViewBag.Empresas = _context.Empresas.ToList(); 
        return View(productoActualizado);
    }

    [HttpPost]
    public IActionResult Eliminar(string id)
    {
        var producto = _context.Productos.Find(id);
        if (producto != null)
        {
            _context.Productos.Remove(producto);
            _context.SaveChanges();
        }
        return RedirectToAction("Crear"); 
    }

}
