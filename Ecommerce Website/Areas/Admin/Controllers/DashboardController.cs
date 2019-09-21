using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_Website.Areas.Admin.Controllers
{
    [AdminAuthenticationFilter]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Error(string err)
        {
            TempData["err"] = err;
            return View();
        }
    }
}