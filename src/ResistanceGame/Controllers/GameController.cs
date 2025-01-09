using Htmx;
using Microsoft.AspNetCore.Mvc;
using ResistanceGame.Web.Hypermedia;

namespace ResistanceGame.Controllers;

public class GameController : Controller
{
    public IActionResult Index(int id)
    {
        var hypermedia = new GameHypermedia(Request, id);
        if (hypermedia.IsNotFound)
        {
            if (!hypermedia.IsHypermedia) return RedirectToAction("Index", "Home");

            Response.Htmx(h => h.Redirect(Url.Action("Index", "Home")!));
            return NoContent();
        }
        // game end - redirect to end

        if (hypermedia.IsHypermedia)
        {
            if (hypermedia.HasOldData()) return NoContent();

            return PartialView("Partial/GamePartial", hypermedia.Model());
        }

        return View(hypermedia.Model());
    }
}