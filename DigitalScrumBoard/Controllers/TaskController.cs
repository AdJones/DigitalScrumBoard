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
            int idStartingPoint = idFromClient.LastIndexOf('_') + 1;
            int id = int.Parse(idFromClient);

            int time = 0;
            bool validTimeReceived = int.TryParse(updatedTaskTime, out time);

            TaskRepository repository = new TaskRepository();
            repository.UpdateTaskDetails(id, updatedTaskText, time, validTimeReceived);
        }
    }
}
