using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
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
        var empresas = _context.Empresas.ToList();
        return View(empresas);
    }

    [HttpPost]
    public IActionResult Crear(Empresa nuevaEmpresa)
    {
        if (ModelState.IsValid)
        {
            _context.Empresas.Add(nuevaEmpresa);
            _context.SaveChanges();

            // Redirige a la acción Crear para mostrar la lista actualizada
            return RedirectToAction("Crear");
        }

        // Si hay un error, regresa a la vista con los datos actuales
        var empresas = _context.Empresas.ToList();
        return View(empresas);
    }
}
