using Microsoft.AspNetCore.Mvc;

public class SharedController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
