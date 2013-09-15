using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScrumBoard.Data;

namespace DigitalScrumBoard.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Avatar URL")]
        public string ImageURL { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Team")]
        public int Team { get; set; }

        [Display(Name = "Select your team")]
        public List<SelectListItem> teams = new List<SelectListItem>();

        public UserViewModel()
        {
        }

        public UserViewModel(List<Team> teams)
        {
            foreach (Team t in teams)
            {
                SelectListItem item = new SelectListItem();
                item.Text = t.Name;
                item.Value = t.ID.ToString();
                this.teams.Add(item);
            }
        }

        public bool IsValid(User user)
        {
            if (user.Password == Helpers.SHA1.Encode(this.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}