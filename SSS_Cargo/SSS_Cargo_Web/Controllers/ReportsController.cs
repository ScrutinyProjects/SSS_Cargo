﻿using CargoBE.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

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
                XElement xelement = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "/CargoConfig.xml");
                IEnumerable<XElement> elements = xelement.Elements();
                foreach (var element in elements)
                {
                    if (element.Name.ToString().ToLower() == "bookingreportrequesttype")
                        ViewBag.RequestType = element.Value;
                }

                return View();
            }
        }

        public ActionResult NotLoadedReport()
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
                XElement xelement = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "/CargoConfig.xml");
                IEnumerable<XElement> elements = xelement.Elements();
                foreach (var element in elements)
                {
                    if (element.Name.ToString().ToLower() == "notloadedreportrequesttype")
                        ViewBag.RequestType = element.Value;
                }

                return View();
            }
        }

        public ActionResult NotDeliveredReport()
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
                XElement xelement = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "/CargoConfig.xml");
                IEnumerable<XElement> elements = xelement.Elements();
                foreach (var element in elements)
                {
                    if (element.Name.ToString().ToLower() == "notdeliveredreportrequesttype")
                        ViewBag.RequestType = element.Value;
                }

                return View();
            }
        }

    }
}