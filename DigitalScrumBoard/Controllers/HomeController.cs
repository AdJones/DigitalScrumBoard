using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DigitalScrumBoard.Data;
using DigitalScrumBoard.Models.ViewModels;
using DigitalScrumBoard.Repositories;

namespace DigitalScrumBoard.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.Page = "home";
        }
        
        //
        // GET: /Home/

        [Authorize]
        public ActionResult Index()
        {
            TeamRepository teamRepo = new TeamRepository();
            Team userTeam = teamRepo.GetTeam(User.Identity.Name);
            List<Sprint> sprints = teamRepo.GetSprints(userTeam.ID);

            HomeViewModel model = new HomeViewModel(userTeam, sprints);
            return View(model);
        }

    }
}
