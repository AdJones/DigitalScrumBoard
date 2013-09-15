using DigitalScrumBoard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalScrumBoard.Repositories
{
    public class SprintRepository
    {
        DigitalScrumBoardDataClassesDataContext context = new DigitalScrumBoardDataClassesDataContext();
        public Sprint GetSprint(int sprintId)
        {
            return context.Sprints.SingleOrDefault(s => s.ID == sprintId);
        }

        public Story CreateStory(string text, int sprintId)
        {
            Story s = new Story();
            s.SprintId = sprintId;
            s.Text = text;
            context.Stories.InsertOnSubmit(s);
            context.SubmitChanges();
            return s;
        }

        public List<Story> GetStories(int sprintId)
        {
            return context.Stories.Where(s => s.SprintId == sprintId).ToList<Story>();
        }

        public void AddBoard(string goals, string name, DateTime start, DateTime end, int teamId)
        {
            Sprint s = new Sprint();
            s.Name = name;
            s.StartDate = start;
            s.EndDate = end;
            s.Goals = goals;
            s.TeamId = teamId;
            context.Sprints.InsertOnSubmit(s);
            context.SubmitChanges();
        }
    }
}