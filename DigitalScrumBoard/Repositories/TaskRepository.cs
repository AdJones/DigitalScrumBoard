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
                return context.Tasks.Where(t => t.SprintID == sprintId).ToList<Task>();
            else
                return context.Tasks.ToList<Task>();
        }

        public void UpdateTaskCoords(int taskId, int left, int top, string droppedIntoColId)
        {
            if (context.Tasks.Count(t => t.ID == taskId) == 1)
            {
                Task taskToUpdate = context.Tasks.Single(t => t.ID == taskId);
                taskToUpdate.Left = left;
                taskToUpdate.Top = top;
                taskToUpdate.CurrentCol = droppedIntoColId;
                context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
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
    }
}