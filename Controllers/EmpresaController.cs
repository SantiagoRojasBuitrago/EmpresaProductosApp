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

            return RedirectToAction("Crear");
        }

        var empresas = _context.Empresas.ToList();
        return View(empresas);
    }

    [HttpGet]
    public IActionResult Editar(string id)
    {
        var empresa = _context.Empresas.Find(id);
        if (empresa == null)
        {
            return NotFound();
        }
        return View(empresa);
    }

    [HttpPost]
    public IActionResult Editar(Empresa empresaActualizada)
    {
        if (ModelState.IsValid)
        {
            _context.Empresas.Update(empresaActualizada);
            _context.SaveChanges();
            return RedirectToAction("Crear");
        }
        return View(empresaActualizada);
    }

    [HttpPost]
    public IActionResult Eliminar(string id)
    {
        var empresa = _context.Empresas.Find(id);
        if (empresa != null)
        {
            _context.Empresas.Remove(empresa);
            _context.SaveChanges();
        }
        return RedirectToAction("Crear");
    }

}
