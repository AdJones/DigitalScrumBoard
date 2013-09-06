using DigitalScrumBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScrumBoard.Models.ViewModels
{
    public class ScrumBoardViewModel
    {
        private List<Task> tasks;

        public List<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        public ScrumBoardViewModel(List<Task> tasks)
        {
            this.tasks = tasks;
        }
    }
}