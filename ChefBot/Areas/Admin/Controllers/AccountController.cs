using ChefBot.Areas.Admin.Attributes;
using ChefBot.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChefBot.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Login()
        {
            if (SessionHelper.User != null)
                return RedirectToAction("Index", "Recipes");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (SessionHelper.User != null)
                return RedirectToAction("Index", "Recipes");

            if (ModelState.IsValid)
            {
                if (SettingsHelper.Username == model.Username &&
                    SettingsHelper.Password == model.Password)
                {
                    SessionHelper.User = new ChefBot.Models.UserSessionModel()
                    {
                        Username = model.Username
                    };

                    return RedirectToAction("Index", "Recipes");
                }
            }
            return View(model);
        }

        [Admin]
        public ActionResult Logout()
        {
            SessionHelper.Clear();
            return RedirectToAction("Login");
        }
    }
}