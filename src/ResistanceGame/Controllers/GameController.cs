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
                return PartialView("Partial/SelectTeamPartial", hypermedia.Model());
            }

            if (hypermedia.IsVoteOfTeam)
            {
                return PartialView("Partial/VoteOfTeamPartial", hypermedia.Model());
            }

            // vote of team - next or new leader

            // execute work

            // result of work
        }

        return View(hypermedia.Model());
    }

    public IActionResult ShowStage(int id)
    {
        var hypermedia = new GameHypermedia(Request, id);
        var model = hypermedia.StagesModel();
        return PartialView("Partial/StagesPartial", model);
    }

    public IActionResult ShowRole(int id, bool isShow = false)
    {
        var hypermedia = new GameHypermedia(Request, id);
        var model = hypermedia.Model();
        model.IsShow = !isShow;
        return PartialView("Partial/RolePartial", model);
    }

    public IActionResult SelectTeamMember(int id, int memberId, bool isSelect)
    {
        var hypermedia = new GameHypermedia(Request, id);
        hypermedia.SelectTeamMember(memberId, isSelect);
        return Index(id);
    }

    public void ConfirmTeam(int id)
    {
        var hypermedia = new GameHypermedia(Request, id);
        if (!hypermedia.MayBeConfirmTeam) return;

        hypermedia.ConfirmTeam();
    }
}