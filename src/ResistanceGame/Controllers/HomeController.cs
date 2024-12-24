using Microsoft.AspNetCore.Mvc;

namespace ResistanceGame.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public string Message()
    {
        return "Привет, Гипермедиа!";
    }
}