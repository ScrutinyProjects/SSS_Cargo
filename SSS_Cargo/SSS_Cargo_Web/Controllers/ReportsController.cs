using CargoBE.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSS_Cargo_Web.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookingReports()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                LoginResponse loginresponse = (LoginResponse)Session["SessionLogin"];
                ViewBag.Name = loginresponse.Name;
                ViewBag.CounterName = loginresponse.CounterName;
                ViewBag.LoginId = loginresponse.LoginId;
                ViewBag.CounterId = loginresponse.CounterId;
                return View();
            }
        }

    }
}