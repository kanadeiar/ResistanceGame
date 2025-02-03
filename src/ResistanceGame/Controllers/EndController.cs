using Microsoft.AspNetCore.Mvc;

namespace ResistanceGame.Controllers;

public class EndController : Controller
{
    public IActionResult Index(int id)
    {
        return View();
    }
}