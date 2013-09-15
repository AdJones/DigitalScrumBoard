﻿using DigitalScrumBoard.Data;
using DigitalScrumBoard.Models.ViewModels;
using DigitalScrumBoard.Models.ViewModels.Partials;
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
            UserRepository userRepo = new UserRepository();

            model.Owner = userRepo.GetUser(task.OwnerID);
            if (string.IsNullOrEmpty(model.Owner.ImageURL))
            {
                model.Owner.ImageURL = "/Content/images/Default.png";
            }

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
            string ownerId = Request.Form["OwnerId"];
            int id = int.Parse(idFromClient);

            int time = 0;
            bool validTimeReceived = int.TryParse(updatedTaskTime, out time);

            TaskRepository repository = new TaskRepository();
            repository.UpdateTaskDetails(id, updatedTaskText, time, validTimeReceived);
            repository.UpdateTaskOwner(id, ownerId);
            repository.UpdateColumnMobileView(id, updatedColumn);
            return RedirectToAction("ScrumBoard", "Sprint");
        }

        [Authorize]
        public ActionResult GetTaskDetails_Ajax()
        {
            string idFromClient = Request["id"];
            int id = 0;
            int.TryParse(idFromClient, out id);
            TaskRepository repository = new TaskRepository();
            UserRepository userRepo = new UserRepository();
            string loggedInUserEmail = User.Identity.Name;

            if (id != 0)
            {
                Task task = repository.GetTaskById(id);
                TaskPartialViewModel model = new TaskPartialViewModel(task);

                model.TeamMembers = new List<SelectListItem>();
                foreach (User u in userRepo.GetTeamMates(loggedInUserEmail))
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = u.Email;
                    item.Value = u.Id.ToString();
                    model.TeamMembers.Add(item);
                }

                return PartialView("TaskDetails", model);
            }
            else
            {
                return PartialView("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Task()
        {
            int sprintId = 0;
            int.TryParse(Request["sprintId"].ToString(), out sprintId);
            SprintRepository sprintRepo = new SprintRepository();
            UserRepository userRepo = new UserRepository();

            ViewBag.Sprint = sprintId;

            string loggedInUserEmail = User.Identity.Name;

            TaskPartialViewModel model = new TaskPartialViewModel(new Task());
            model.Stories = new List<SelectListItem>();
            foreach (Story s in sprintRepo.GetStories(sprintId))
            {
                SelectListItem item = new SelectListItem();
                item.Text = s.Text;
                item.Value = s.ID.ToString();
                model.Stories.Add(item);
            }

            model.TeamMembers = new List<SelectListItem>();
            foreach (User u in userRepo.GetTeamMates(loggedInUserEmail))
            {
                SelectListItem item = new SelectListItem();
                item.Text = u.Email;
                item.Value = u.Id.ToString();
                model.TeamMembers.Add(item);
            }

            return PartialView(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Task(TaskPartialViewModel model)
        {
            string idFromClient = Request.Form["sprintId"];
            int sprintId = 0;
            int.TryParse(Request["sprintId"].ToString(), out sprintId);
            TaskRepository taskRepo = new TaskRepository();

            if (ModelState.IsValid)
            {
                taskRepo.CreateTask(model.Text, model.Type, model.StoryId, model.OwnerId, model.TimeRemaining);
            }

            return RedirectToAction("ScrumBoard", "Sprint", new { id = sprintId });
        }
    }
}
