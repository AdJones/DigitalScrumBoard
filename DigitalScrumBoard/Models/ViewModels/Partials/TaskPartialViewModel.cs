using DigitalScrumBoard.Business;
using DigitalScrumBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DigitalScrumBoard.Models.ViewModels
{
    public class TaskPartialViewModel
    {
        private int id;
        private int left;
        private int top;
        private TaskType type;
        private string text;
        private int storyId;
        private int timeRemaining;
        private string currColumn;

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid number of hours")]
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

        public int StoryId
        {
            get { return storyId; }
            set { storyId = value; }
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

        public string CurrColumn
        {
            get { return currColumn; }
            set { currColumn = value; }
        }

        private List<SelectListItem> items;

        public List<SelectListItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public TaskPartialViewModel(Task t)
        {
            this.id = t.ID;
            this.left = t.Left;
            this.top = t.Top;
            this.Type = (TaskType)t.Type;
            this.text = t.Text;
            this.timeRemaining = t.TimeRemaining;
            this.storyId = t.StoryID;
            this.currColumn = t.CurrentCol;

            string[] availableCols = new string[] { "NotDoneCol", "InProgressCol", "InTestCol", "DoneCol" };
            this.items = new List<SelectListItem>();
            foreach (string col in availableCols)
            {
                SelectListItem item = new SelectListItem();
                item.Text = col;
                item.Value = col;
                this.items.Add(item);
            }
        }
    }
}