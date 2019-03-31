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
            //if (Session["UserLogin"] == null)
            //{
            //    return Redirect("/cargo/index");
            //}
            //else
            //{
                return View();
            //}
        }

        public ActionResult booking()
        {
            //if (Session["UserLogin"] == null)
            //{
            //    return Redirect("/cargo/index");
            //}
            //else
            //{
            return View();
            //}
        }

        public ActionResult loading()
        {
            //if (Session["UserLogin"] == null)
            //{
            //    return Redirect("/cargo/index");
            //}
            //else
            //{
            return View();
            //}
        }

        public ActionResult tobereceive()
        {
            //if (Session["UserLogin"] == null)
            //{
            //    return Redirect("/cargo/index");
            //}
            //else
            //{
            return View();
            //}
        }

        public ActionResult receive()
        {
            //if (Session["UserLogin"] == null)
            //{
            //    return Redirect("/cargo/index");
            //}
            //else
            //{
            return View();
            //}
        }

        public ActionResult delivery()
        {
            //if (Session["UserLogin"] == null)
            //{
            //    return Redirect("/cargo/index");
            //}
            //else
            //{
            return View();
            //}
        }
    }
}