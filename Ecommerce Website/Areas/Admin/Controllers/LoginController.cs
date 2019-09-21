using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce_Website.Models;

namespace Ecommerce_Website.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        DBProjectEntities db = new DBProjectEntities();

        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginMaster data)
        {
            try
            {

                var Logindata = db.LoginMasters.SingleOrDefault(a => a.LoginName == data.LoginName && a.LoginPassword == data.LoginPassword);
                if (Logindata != null)
                {
                    Session["adminId"] = Logindata.LoginId;
                    TempData["msg"] = "Login Done!";

                    return RedirectToAction("Index", "CategoryMasters");
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