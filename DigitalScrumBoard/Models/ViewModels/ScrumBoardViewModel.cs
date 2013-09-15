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
        private int sprintId;

        public List<Story> Stories
        {
            get { return stories; }
            set { stories = value; }
        }

        public int SprintId
        {
            get { return sprintId; }
            set { sprintId = value; }
        }

        public ScrumBoardViewModel(int sprintId, List<Story> stories)
        {
            this.sprintId = sprintId;
            this.stories = stories;
        }
    }
}