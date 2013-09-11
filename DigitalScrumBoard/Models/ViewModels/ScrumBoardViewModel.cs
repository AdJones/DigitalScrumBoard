using DigitalScrumBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScrumBoard.Models.ViewModels
{
    public class ScrumBoardViewModel
    {
        private List<Story> stories;

        public List<Story> Stories
        {
            get { return stories; }
            set { stories = value; }
        }

        public ScrumBoardViewModel(List<Story> stories)
        {
            this.stories = stories;
        }
    }
}