using CargoBE.Responses;
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
            if (Session["SessionLogin"] != null)
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
            return Redirect("/account/login");
        }

        [HttpPost]
        public ActionResult SaveLoginDetails(LoginResponse objresponse)
        {
            try
            {
                objManageSessions.SaveUserSessions(objresponse);                
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = "Oops!! unable to process your request";
            }
            return Json(new
            {
                StatusId = objresponse.StatusId,
                StatusMessage = objresponse.StatusMessage
            });
        }
    }
}