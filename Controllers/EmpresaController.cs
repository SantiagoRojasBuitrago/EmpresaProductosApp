using Microsoft.AspNetCore.Mvc;

public class EmpresaController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmpresaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Empresa empresa)
    {
        if (ModelState.IsValid)
        {
            _context.Empresas.Add(empresa);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(empresa);
    }
}
