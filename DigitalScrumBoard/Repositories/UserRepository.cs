using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using DigitalScrumBoard.Data;
using DigitalScrumBoard.Models.ViewModels;

namespace DigitalScrumBoard.Repositories
{
    public class UserRepository
    {
        DigitalScrumBoardDataClassesDataContext context = new DigitalScrumBoardDataClassesDataContext();

        public User GetUser(int id)
        {
            return context.Users.SingleOrDefault(u => u.Id == id);
        }

        public User GetUser(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public void AddUser(UserViewModel user)
        {
            string passwordToAdd = Helpers.SHA1.Encode(user.Password);
            User newUser = new User();
            newUser.Password = passwordToAdd;
            newUser.Email = user.Email;
            newUser.TeamId = user.Team;
            context.Users.InsertOnSubmit(newUser);
            context.SubmitChanges();
        }

        public List<User> GetTeamMates(string email)
        {
            User user = context.Users.SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
            Team userTeam = context.Teams.SingleOrDefault(t => t.Users.Contains(user));
            return userTeam.Users.ToList<User>();
        }
    }
}