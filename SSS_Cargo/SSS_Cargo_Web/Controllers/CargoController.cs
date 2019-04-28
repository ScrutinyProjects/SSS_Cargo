using CargoBE;
using CargoBE.Responses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public ActionResult bookingsuccess(string param1, string param2)
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login"); 
            }
            else
            {
                ViewBag.BookingId = param1;
                ViewBag.BookingSerialNumber = CommonMethods.URLKeyDecrypt(param2);
                return View();
            }
        }

        public ActionResult printbookingreceipt(string param1)
        {
            if (Session["SessionLogin"] == null)
            {
                return Redirect("/account/login");
            }
            else
            {
                ViewBag.BookingId = param1;
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
                ViewBag.ContactNumber = loginresponse.ContactNumber;
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

        [HttpPost]
        public ActionResult GenerateBarCode(string barcode)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Bitmap bitMap = new Bitmap(barcode.Length * 40, 80))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        Font oFont = new Font("IDAutomationHC39M", 16);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        SolidBrush blackBrush = new SolidBrush(Color.DarkBlue);
                        graphics.DrawString(barcode, oFont, blackBrush, point);
                    }

                    bitMap.Save(memoryStream, ImageFormat.Jpeg);

                    ViewBag.BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return View();
        }
    }
}