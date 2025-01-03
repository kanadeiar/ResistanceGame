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
            if (!hypermedia.IsHtmx) return RedirectToAction("Index", "Home");

            Response.Htmx(h => h.Redirect(Url.Action("Index", "Home")!));
            return NoContent();
        }

        if (hypermedia.IsHtmx)
        {
            if (hypermedia.HasOldData()) return NoContent();
            return PartialView("Partial/WaitPartial", hypermedia.Model());
        }

        return View(hypermedia.Model());
    }
}