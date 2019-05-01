using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoBE.Responses;
using System.Xml;
using System.Xml.Linq;

namespace SSS_Cargo_Web.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Index()
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                LoginResponse loginresponse = (LoginResponse)Session["SessionLogin"];
                XElement xelement = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "/CargoConfig.xml");
                IEnumerable<XElement> elements = xelement.Elements();
                foreach (var element in elements)
                {
                    if (element.Name.ToString().ToLower() == "expense")
                    {
                        IEnumerable<XElement> expenseConfig = element.Elements();
                        foreach (var item in expenseConfig)
                        {
                            if (item.Name.ToString().ToLower() == "editroles")
                            {
                                if (item.Value.Split(',').ToArray().Contains(loginresponse.RoleName))
                                {
                                    ViewBag.IsEditable = true;
                                }
                            }
                            if (item.Name.ToString().ToLower() == "deleteroles")
                            {
                                if (item.Value.Split(',').ToArray().Contains(loginresponse.RoleName))
                                {
                                    ViewBag.IsDeletable = true;
                                }
                            }
                        }
                    }

                }
                ViewBag.Name = loginresponse.Name;
                ViewBag.CounterName = loginresponse.CounterName;
                ViewBag.LoginId = loginresponse.LoginId;
                ViewBag.CounterId = loginresponse.CounterId;
                return View();
            }
        }

        public ActionResult ExpenseLock()
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