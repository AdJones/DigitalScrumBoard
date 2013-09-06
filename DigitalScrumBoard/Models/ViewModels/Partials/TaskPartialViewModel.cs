using DigitalScrumBoard.Business;
using DigitalScrumBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScrumBoard.Models.ViewModels
{
    public class TaskPartialViewModel
    {
        private int id;
        private int left;
        private int top;
        private TaskType type;
        private string text;
        private int timeRemaining;

        public int TimeRemaining
        {
            get { return timeRemaining; }
            set { timeRemaining = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public TaskType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public TaskPartialViewModel(Task t)
        {
            this.id = t.ID;
            this.left = t.Left;
            this.top = t.Top;
            this.Type = (TaskType)t.Type;
            this.text = t.Text;
            this.timeRemaining = t.TimeRemaining;
        }
    }
}