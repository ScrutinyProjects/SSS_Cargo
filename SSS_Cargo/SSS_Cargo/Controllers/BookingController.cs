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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Route("getreceivinglocations")]
        [HttpPost]
        public List<CounterMastersResponse> GetReceivingLocations(JObject input)
        {
            List<CounterMastersResponse> objresponse = new List<CounterMastersResponse>();
            try
            {
                objresponse = objBookingBal.GetReceivingLocations(input);
            }
            catch (Exception ex)
            {
                
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("getcalculatedpriceforbooking")]
        [HttpPost]
        public BookingCalculatedPriceResponse GetCalculatedPriceForBooking(JObject input)
        {
            BookingCalculatedPriceResponse objresponse = new BookingCalculatedPriceResponse();
            try
            {
                objresponse = objBookingBal.GetCalculatedPriceForBooking(input);
            }
            catch (Exception ex)
            {
                
            }
            return objresponse;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Route("getbookingsbycounterid")]
        [HttpPost]
        public List<BookingsListByCounter> GetBookingsByCounterId(JObject input)
        {
            List<BookingsListByCounter> objresponse = new List<BookingsListByCounter>();
            try
            {
                objresponse = objBookingBal.GetBookingsByCounterId(input);
            }
            catch (Exception ex)
            {

            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("getcustomerdetailsbymobilenumber")]
        [HttpPost]
        public CustomerDetailsResponse GetCustomerDetailsByMobileNumber(JObject input)
        {
            CustomerDetailsResponse objresponse = new CustomerDetailsResponse();
            try
            {
                objresponse = objBookingBal.GetCustomerDetailsByMobileNumber(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("getbookingdetailsbybookingnumber")]
        [HttpPost]
        public BookingDetailsByBookingNumber GetBookingDetailsByBookingNumber(JObject input)
        {
            BookingDetailsByBookingNumber objresponse = new BookingDetailsByBookingNumber();
            try
            {
                objresponse = objBookingBal.GetBookingDetailsByBookingNumber(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("saveloadingdetails")]
        [HttpPost]
        public SaveRespone InsertLoadingDetails(JObject input)
        {
            SaveRespone objresponse = new SaveRespone();
            try
            {
                objresponse = objBookingBal.InsertLoadingDetails(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("getmastersfortobereceive")]
        [HttpPost]
        public TobeReceiveMastersResponse GetMastersForToBeReceive(JObject input)
        {
            TobeReceiveMastersResponse objresponse = new TobeReceiveMastersResponse();
            try
            {
                objresponse = objBookingBal.GetMastersForToBeReceive(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("savetobereceivedetails")]
        [HttpPost]
        public SaveRespone InsertToBeReceiveDetails(JObject input)
        {
            SaveRespone objresponse = new SaveRespone();
            try
            {
                objresponse = objBookingBal.InsertToBeReceiveDetails(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("getmastersforreceiving")]
        [HttpPost]
        public ReceivingMastersResponse GetMastersForReceiving(JObject input)
        {
            ReceivingMastersResponse objresponse = new ReceivingMastersResponse();
            try
            {
                objresponse = objBookingBal.GetMastersForReceiving(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("gettobereceiveddetailsbybookingnumber")]
        [HttpPost]
        public ToBereceiveDetailsByBookingNumber GetToBeReceivedDetailsByBookingNumber(JObject input)
        {
            ToBereceiveDetailsByBookingNumber objresponse = new ToBereceiveDetailsByBookingNumber();
            try
            {
                objresponse = objBookingBal.GetToBeReceivedDetailsByBookingNumber(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("savereceivingdetails")]
        [HttpPost]
        public SaveRespone InsertReceivingDetails(JObject input)
        {
            SaveRespone objresponse = new SaveRespone();
            try
            {
                objresponse = objBookingBal.InsertReceivingDetails(input);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        [AllowAnonymous]
        [Route("getbookingdetailstoprintbybookingid")]
        [HttpPost]
        public BookingDetailsForPrintResponse GetBookingDetailsToPrintByBookingId(JObject input)
        {
            BookingDetailsForPrintResponse objresponse = new BookingDetailsForPrintResponse();
            try
            {
                objresponse = objBookingBal.GetBookingDetailsToPrintByBookingId(input);
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