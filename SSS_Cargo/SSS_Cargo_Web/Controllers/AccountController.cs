using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSS_Cargo_Web.Controllers
{
    public class AccountController : Controller
    {
        ManageSessions objManageSessions = new ManageSessions();

        // GET: Account
        public ActionResult login()
        {
            if (Session["UserLogin"] != null)
            {
                return Redirect("/cargo/index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult logout()
        {
            Session.Abandon();
            return Redirect("/login");
        }
    }
}