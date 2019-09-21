using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Ecommerce_Website.AdminAuthenticationFilter;

namespace Ecommerce_Website.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        [UserAuthenticationFilter]
        // GET: User/Home
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login","SignIn");
        }
    }
}