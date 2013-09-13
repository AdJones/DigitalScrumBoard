using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalScrumBoard.Data;

namespace DigitalScrumBoard.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Sprint> sprints;
        public Team team;

        public HomeViewModel()
        {
        }

        public HomeViewModel(Team userTeam, List<Sprint> sprints)
        {
            this.team = userTeam;
            this.sprints = sprints;
        }
    }
}