using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DigitalScrumBoard.Data;
using DigitalScrumBoard.Models.ViewModels;
using DigitalScrumBoard.Repositories;

namespace DigitalScrumBoard.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public UserController()
        {
            ViewBag.Page = "user";
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            TeamRepository repository = new TeamRepository();
            UserViewModel model = new UserViewModel(repository.GetTeams());
            return View(model);
        }

        public ActionResult Login(UserViewModel userModel)
        {
            bool createPersistentCookie = true;
            UserRepository repository = new UserRepository();
            if (ModelState.IsValid)
            {
                if (userModel.IsValid(repository.GetUser(userModel.Email)))
                {
                    FormsAuthentication.SetAuthCookie(userModel.Email, createPersistentCookie);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult Register(UserViewModel userModel)
        {
            bool createPersistentCookie = true;
            UserRepository repository = new UserRepository();

            if (ModelState.IsValid)
            {
                repository.AddUser(userModel);
                FormsAuthentication.SetAuthCookie(userModel.Email, createPersistentCookie);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "User");
        }
    }
}
