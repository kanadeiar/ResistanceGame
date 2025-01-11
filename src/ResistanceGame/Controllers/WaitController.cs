using Htmx;
using Microsoft.AspNetCore.Mvc;
using ResistanceGame.Web.Hypermedia;

namespace ResistanceGame.Controllers;

public class WaitController : Controller
{
    public IActionResult Index(int id)
    {
        var hypermedia = new WaitHypermedia(Request, id);
        if (hypermedia.IsNotFound)
        {
            if (!hypermedia.IsHypermedia) return RedirectToAction("Index", "Home");

            Response.Htmx(h => h.Redirect(Url.Action("Index", "Home")!));
            return NoContent();
        }
        if (hypermedia.IsGameStarted)
        {
            Response.Htmx(h => h.Redirect(Url.Action("Index", "Game", new { id })!));
            return NoContent();
        }

        if (hypermedia.IsHypermedia)
        {
            if (hypermedia.HasOldData()) return NoContent();
            
            return PartialView("Partial/WaitPartial", hypermedia.Model());
        }

        return View(hypermedia.Model());
    }
    
    public IActionResult SwitchReady(int id)
    {
        var hypermedia = new WaitHypermedia(Request, id);
        hypermedia.SwitchReady();

        return PartialView("Partial/ReadyPartial", hypermedia.Model());
    }

    public void Start(int id)
    {
        var hypermedia = new WaitHypermedia(Request, id);
        hypermedia.Start();
    }
}