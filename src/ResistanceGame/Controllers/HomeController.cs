using Microsoft.AspNetCore.Mvc;
using ResistanceGame.Web.Hypermedia;
using ResistanceGame.Web.Models;

namespace ResistanceGame.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(RegistrationWebModel model)
    {
        var hypermedia = new RegistrationHypermedia(Request, ModelState, model);

        if (hypermedia.IsHtmx) return PartialView("Partial/RegistrationPartial", model);
        if (hypermedia.IsInvalid) return View("Index", model);

        var id = hypermedia.RegisterNewMember();

        return RedirectToAction("Index", "Wait", new { id });
    }
}