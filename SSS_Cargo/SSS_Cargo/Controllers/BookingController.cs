using CargoBAL;
using CargoBE.Responses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SSS_Cargo.Controllers
{
    [RoutePrefix("api/booking")]
    public class BookingController : ApiController
    {
        #region Members

        BookingBal objBookingBal = new BookingBal();

        #endregion

        #region APIs

        [Authorize]
        [Route("getmasters")]
        [HttpPost]
        public BookingMastersResponse GetMasters(JObject input)
        {
            BookingMastersResponse objresponse = new BookingMastersResponse();
            try
            {
                objresponse = objBookingBal.GetMastersForBooking(input);
            }
            catch(Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [Authorize]
        [Route("savebookingdetails")]
        [HttpPost]
        public BookingSaveResponse InsertBookingDetails(JObject input)
        {
            BookingSaveResponse objresponse = new BookingSaveResponse();
            try
            {
                objresponse = objBookingBal.InsertBookingDetails(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        #endregion
    }
}