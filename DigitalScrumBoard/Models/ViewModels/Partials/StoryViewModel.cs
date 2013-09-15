using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScrumBoard.Models.ViewModels.Partials
{
    public class StoryViewModel
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private string satisfactionConditions;

        public string SatisfactionConditions
        {
            get { return satisfactionConditions; }
            set { satisfactionConditions = value; }
        }


        public StoryViewModel()
        {
        }

    }
}