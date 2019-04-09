﻿using CargoBE.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSS_Cargo_Web.Controllers
{
    public class CargoController : Controller
    {
        // GET: Cargo
        public ActionResult index()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult booking()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                LoginResponse loginresponse = (LoginResponse)Session["SessionLogin"];
                ViewBag.CounterName = loginresponse.CounterName;
                ViewBag.CounterId = loginresponse.CounterId;
                ViewBag.LoginId = loginresponse.LoginId;
                return View();
            }
        }

        public ActionResult bookingsuccess(string param1)
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login"); 
            }
            else
            {
                ViewBag.BookingSerialNumber = param1;
                return View();
            }
        }

        public ActionResult loading()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                LoginResponse loginresponse = (LoginResponse)Session["SessionLogin"];
                ViewBag.CounterName = loginresponse.CounterName;
                ViewBag.CounterId = loginresponse.CounterId;
                ViewBag.LoginId = loginresponse.LoginId;
                return View();
            }
        }

        public ActionResult tobereceive()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                LoginResponse loginresponse = (LoginResponse)Session["SessionLogin"];
                ViewBag.CounterName = loginresponse.CounterName;
                ViewBag.CounterId = loginresponse.CounterId;
                ViewBag.LoginId = loginresponse.LoginId;
                return View();
            }
        }

        public ActionResult receive()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                LoginResponse loginresponse = (LoginResponse)Session["SessionLogin"];
                ViewBag.CounterName = loginresponse.CounterName;
                ViewBag.CounterId = loginresponse.CounterId;
                ViewBag.LoginId = loginresponse.LoginId;
                return View();
            }
        }

        public ActionResult delivery()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                LoginResponse loginresponse = (LoginResponse)Session["SessionLogin"];
                ViewBag.CounterName = loginresponse.CounterName;
                ViewBag.CounterId = loginresponse.CounterId;
                ViewBag.LoginId = loginresponse.LoginId;
                return View();
            }
        }
    }
}