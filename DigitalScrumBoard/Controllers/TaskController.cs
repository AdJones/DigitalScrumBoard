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
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        [Authorize]
        public ActionResult Index(Task task)
        {
            TaskPartialViewModel model = new TaskPartialViewModel(task);

            return PartialView(model);
        }

        public void UpdateTaskCoords_Ajax()
        {
            DigitalScrumBoardDataClassesDataContext context = new DigitalScrumBoardDataClassesDataContext();
            string idFromClient = Request["id"];
            string droppedIntoCol = Request["droppedIntoCol"];
            int id = int.Parse(idFromClient);
            int left = (int)Math.Floor(decimal.Parse(Request["left"]));
            int top = (int)Math.Floor(decimal.Parse(Request["top"]));

            TaskRepository repository = new TaskRepository();
            repository.UpdateTaskCoords(id, left, top, droppedIntoCol);
        }

        public void UpdateTaskDetails_Ajax()
        {
            string updatedTaskText = Request["taskText"];
            string updatedTaskTime = Request["taskTime"];
            string idFromClient = Request["id"];
            int id = int.Parse(idFromClient);

            int time = 0;
            bool validTimeReceived = int.TryParse(updatedTaskTime, out time);

            TaskRepository repository = new TaskRepository();
            repository.UpdateTaskDetails(id, updatedTaskText, time, validTimeReceived);
        }

        public ActionResult UpdateTaskFromMobileView()
        {
            string idFromClient = Request.Form["id"];
            string updatedTaskText = Request.Form["Text"];
            string updatedTaskTime = Request.Form["TimeRemaining"];
            string updatedColumn = Request.Form["CurrColumn"];
            int id = int.Parse(idFromClient);

            int time = 0;
            bool validTimeReceived = int.TryParse(updatedTaskTime, out time);

            TaskRepository repository = new TaskRepository();
            repository.UpdateTaskDetails(id, updatedTaskText, time, validTimeReceived);
            repository.UpdateColumnMobileView(id, updatedColumn);
            return RedirectToAction("ScrumBoard", "Sprint");
        }

        public ActionResult GetTaskDetails_Ajax()
        {
            string idFromClient = Request["id"];
            int id = 0;
            int.TryParse(idFromClient, out id);
            TaskRepository repository = new TaskRepository();

            if (id != 0)
            {
                Task task = repository.GetTaskById(id);
                TaskPartialViewModel model = new TaskPartialViewModel(task);
                return PartialView("TaskDetails", model);
            }
            else
            {
                return PartialView("Error");
            }
        }
    }
}
