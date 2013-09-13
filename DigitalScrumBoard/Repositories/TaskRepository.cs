using DigitalScrumBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScrumBoard.Repositories
{
    public class TaskRepository
    {
        DigitalScrumBoardDataClassesDataContext context = new DigitalScrumBoardDataClassesDataContext();
        public List<Task> GetScrumTasks(int sprintId)
        {
            if (sprintId != 0)
                return context.Tasks.Where(t => t.Story.SprintId == sprintId).ToList<Task>();
            else
                return context.Tasks.ToList<Task>();
        }

        public List<Story> GetScrumStories(int sprintId)
        {
            if (sprintId != 0)
                return context.Stories.Where(s => s.SprintId == sprintId).ToList<Story>();
            else
                return context.Stories.ToList<Story>();
        }

        public void UpdateTaskCoords(int taskId, int left, int top, string droppedIntoColId)
        {
            if (context.Tasks.Count(t => t.ID == taskId) == 1)
            {
                Task taskToUpdate = context.Tasks.Single(t => t.ID == taskId);
                taskToUpdate.Left = left;
                taskToUpdate.Top = top;
                taskToUpdate.CurrentCol = droppedIntoColId;
                context.SubmitChanges();
            }
        }

        public void UpdateTaskDetails(int taskId, string taskText, int timeRemaining, bool updateTime)
        {
            if (this.context.Tasks.Any(t => t.ID == taskId))
            {
                Task task = context.Tasks.Single(t => t.ID == taskId);
                task.Text = taskText.Trim().Replace("&nbsp;", " ");
                if (updateTime)
                {
                    task.TimeRemaining = timeRemaining;
                }
                this.context.SubmitChanges();
            }
        }

        public void UpdateColumnMobileView(int id, string columnName)
        {
            Task task = this.context.Tasks.SingleOrDefault(t => t.ID == id);

            int left = 0;

            //TODO: Fuuuugly... needs fixing if and when columns are made to be configurable
            switch (columnName)
            {
                case "NotDoneCol":
                    left = 5;
                    break;
                case "InProgressCol":
                    left = 30;
                    break;
                case "InTestCol":
                    left = 55;
                    break;
                case "DoneCol":
                    left = 80;
                    break;
            }

            if (left != 0)
            {
                task.Left = left;
            }
            task.CurrentCol = columnName;
            this.context.SubmitChanges();
            
        }

        public Task GetTaskById(int id)
        {
            return this.context.Tasks.SingleOrDefault(t => t.ID == id);
        }
    }
}