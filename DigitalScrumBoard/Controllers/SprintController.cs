using DigitalScrumBoard.Data;
using DigitalScrumBoard.Models.ViewModels;
using DigitalScrumBoard.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalScrumBoard.Controllers
{
    public class SprintController : Controller
    {
        //
        // GET: /Sprint/ScrumBoard/

        [Authorize]
        public ActionResult ScrumBoard()
        {
            ViewBag.Page = "board";

            TaskRepository repository = new TaskRepository();
            int sprintId = 0;

            if (RouteData.Values["id"] != null)
            {
                int.TryParse(RouteData.Values["id"].ToString(), out sprintId);
            }
            List<Story> stories = repository.GetScrumStories(sprintId);

            ScrumBoardViewModel model = new ScrumBoardViewModel(stories);
            return View(model);
        }

        [Authorize]
        public ActionResult BurnDown(int sprintId)
        {
            // List of int with hours left

            ViewData["Chart"] = new List<int> { 1,2,3 } ;
            // List of int with days

            return View();
        }
    }
}
