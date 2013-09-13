using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitalScrumBoard.Data;

namespace DigitalScrumBoard.Repositories
{
    public class TeamRepository
    {
        DigitalScrumBoardDataClassesDataContext context = new DigitalScrumBoardDataClassesDataContext();
        public List<Team> GetTeams()
        {
            return context.Teams.ToList();
        }

        public Team GetTeam(string email)
        {
            User user = context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
            return context.Teams.SingleOrDefault(t => t.Users.Contains(user));
        }

        public List<Sprint> GetSprints(int teamID)
        {
            return context.Sprints.Where(s => s.TeamId == teamID).ToList();
        }
    }
}