using Microsoft.AspNetCore.Mvc;

public class SharedController : Controller
{
    // Otros métodos...

    public IActionResult Index()
    {
        // Lógica para obtener datos necesarios para la vista
        return View();
    }
}
