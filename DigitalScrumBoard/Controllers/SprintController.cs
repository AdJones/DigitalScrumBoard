using DigitalScrumBoard.Data;
using DigitalScrumBoard.Models.ViewModels;
using DigitalScrumBoard.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using DigitalScrumBoard.Models.ViewModels.Partials;

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

            ScrumBoardViewModel model = new ScrumBoardViewModel(sprintId, stories);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddBoard()
        {
            ViewBag.Page = "board";

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddBoard(BoardViewModel boardModel)
        {
            if (ModelState.IsValid)
            {
                SprintRepository sprintRepo = new SprintRepository();
                TeamRepository teamRepo = new TeamRepository();
                Team userTeam = teamRepo.GetTeam(User.Identity.Name);
                sprintRepo.AddBoard(boardModel.Goals, boardModel.Name, boardModel.StartDate, boardModel.EndDate, userTeam.ID);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("AddBoard", "Sprint");
            }
        }

        [Authorize]
        public ActionResult BurnDown()
        {
            int sprintId = 0;
            int.TryParse(Request["sprintId"].ToString(), out sprintId);
            SprintRepository repository = new SprintRepository();
            Sprint sprint = repository.GetSprint(sprintId);
            
            if (sprint != null)
            {
                List<DateTime> dates = Helpers.DateTimeHelpers.GetAllDatesBetween(sprint.StartDate.Value, sprint.EndDate.Value).ToList<DateTime>();
                string[] dateStrings = new string[dates.Count];
                object[] values = new object[dates.Count];
                for (int i = 0; i < dates.Count; i++)
                {
                    dateStrings[i] = dates[i].ToString("dd/MM/yyyy");
                }

                for (int i = dates.Count; i > 0; i--)
                {
                    values[dates.Count - i] = i * 5;
                }

                XAxisLabels labels = new XAxisLabels();
                labels.Rotation = -45;
                labels.Align = HorizontalAligns.Right;

                Title title = new Title();
                title.Text = "Burndown for " + sprint.Name;
                Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                    .SetTitle(title)
                    .SetXAxis(new XAxis
                    {
                        Categories = dateStrings,
                        Id = "Days of sprint",
                        Labels = labels
                    })
                    .SetSeries(new Series
                    {
                        Data = new DotNet.Highcharts.Helpers.Data(values),
                        Name = "Hours Remaining"
                    });
                return View(chart);
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Story()
        {
            int sprintId = 0;
            int.TryParse(Request["sprintId"].ToString(), out sprintId);
            ViewBag.Sprint = sprintId;

            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Story(StoryViewModel storyModel)
        {
            string idFromClient = Request.Form["sprintId"];
            int sprintId = 0;
            int.TryParse(Request["sprintId"].ToString(), out sprintId);

            SprintRepository sprintRepos = new SprintRepository();
            TaskRepository taskRepos = new TaskRepository();
            Story createdStory = sprintRepos.CreateStory(storyModel.Text, sprintId);
            taskRepos.CreateTask(storyModel.Text, Business.TaskType.UserStory, createdStory.ID, 0, 0);
            taskRepos.CreateTask(storyModel.SatisfactionConditions, Business.TaskType.SatisfactionConditions, createdStory.ID, 0, 0);

            return RedirectToAction("ScrumBoard", "Sprint", new { id = sprintId });
        }
    }
}
