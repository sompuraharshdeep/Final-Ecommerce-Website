using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce_Website.Models;

namespace Ecommerce_Website.Areas.User.Controllers
{
    public class SignInController : Controller
    {
        DBProjectEntities db = new DBProjectEntities();
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            try
            {

                var Logindata = db.UserLogins.SingleOrDefault(a => a.Useremail == login.Useremail && a.UserPassword == login.UserPassword);
                if (Logindata != null)
                {
                    Session["UserId"] = Logindata.UserId;
                    TempData["msg"] = "Login Done!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["err"] = "Username or Password is Wrong!!";

                }
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
            }
            return View();
        }
    }
}