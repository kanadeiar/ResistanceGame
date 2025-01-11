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
        if (hypermedia.IsEnd)
        {
            if (!hypermedia.IsHypermedia) return RedirectToAction("Index", "End", new { id });

            Response.Htmx(h => h.Redirect(Url.Action("Index", "End", new { id })!));
            return NoContent();
        }

        if (hypermedia.IsHypermedia)
        {
            if (hypermedia.HasOldData()) return NoContent();

            if (hypermedia.IsSelectTeam)
            {
                if (hypermedia.IsLeader)
                {
                    return PartialView("Partial/SelectTeamPartial", hypermedia.Model());
                }

                return PartialView("Partial/NewLeaderInfoPartial", hypermedia.Model());
            }

            // vote of team - next or new leader

            // execute work

            // result of work
        }

        return View(hypermedia.Model());
    }
}