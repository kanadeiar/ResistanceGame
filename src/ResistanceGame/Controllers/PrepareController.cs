using Microsoft.AspNetCore.Mvc;

namespace ResistanceGame.Controllers;

public class PrepareController : Controller
{
    public IActionResult Index(int id)
    {
        return View();
    }
}