using Microsoft.AspNetCore.Mvc;

namespace ResistanceGame.Controllers;

public class GameController : Controller
{
    public IActionResult Index(int id)
    {


        return View();
    }
}