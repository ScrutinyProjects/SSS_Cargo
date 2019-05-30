using CargoBAL;
using CargoBE.Responses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSS_Cargo.Controllers
{
    [RoutePrefix("api/reports")]
    public class ReportsController : ApiController
    {
        #region Members

        ReportsBal objReportsBal = new ReportsBal();

        #endregion

        #region APIS
        [Route("{loginid}/{counterid}/getbookingreportsdata")]
        [HttpGet]
        public object GetBookingReportsData(string loginId, string counterId, string requestType)
        {
            List<CounterMastersResponse> lstCounterMastersResponse = null;
            List<GCTypesResponse> lstGCTypesResponse = null;
            List<BookingStatusResponse> lstBookingStatusResponse = null;
            try
            {
                lstCounterMastersResponse = objReportsBal.GetMyLocations(loginId, counterId, requestType);
                lstGCTypesResponse = objReportsBal.GetGCTypes();
                lstBookingStatusResponse = objReportsBal.GetBookingStatus();
            }
            catch (Exception ex)
            {

            }
            return new { Locations = lstCounterMastersResponse, GCTypes = lstGCTypesResponse, BookingStatus = lstBookingStatusResponse };
        }


        [Route("getbookingreport")]
        [HttpPost]
        public List<BookingReportResponse> GetBookingReport(JObject input)
        {
            List<BookingReportResponse> lstBookingReportResponse = null;
            try
            {
                lstBookingReportResponse = objReportsBal.GetBookingReport(input);

            }
            catch (Exception ex)
            {

            }
            return lstBookingReportResponse;
        }

        [Route("updatebookingstatus")]
        [HttpPost]
        public bool UpdateBookingStatus(JObject input)
        {
            bool result = false;
            try
            {
                result = objReportsBal.UpdateBookingStatus(input);

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        [Route("bookingpricedetails")]
        [HttpGet]
        public BookingPriceResponse BookingPriceDetails(int bookingId)
        {
            BookingPriceResponse objBookingPriceResponse = null;
            try
            {
                objBookingPriceResponse = objReportsBal.BookingPriceDetails(bookingId);

            }
            catch (Exception ex)
            {

            }
            return objBookingPriceResponse;
        }

        [Route("{loginid}/{counterid}/getnotloadedreportsdata")]
        [HttpGet]
        public object GetNotLoadedReportsData(string loginId, string counterId, string requestType)
        {
            List<CounterMastersResponse> lstCounterMastersResponse = null;
            try
            {
                lstCounterMastersResponse = objReportsBal.GetMyLocations(loginId, counterId, requestType);
            }
            catch (Exception ex)
            {

            }
            return new { Locations = lstCounterMastersResponse };
        }


        [Route("getnotloadedreport")]
        [HttpPost]
        public DataSet GetNotLoadedReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsBal.GetNotLoadedReport(input);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        [Route("getnotdeliveredreport")]
        [HttpPost]
        public DataSet GetNotDeliveredReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsBal.GetNotDeliveredReport(input);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }


        [Route("{loginid}/{counterid}/getcashreportsdata")]
        [HttpGet]
        public DataSet GetCashReportsData(string loginId, string counterId, string requestType)
        {
            DataSet dsUsers = null;
            try
            {
                dsUsers = objReportsBal.GetUsers();
            }
            catch (Exception ex)
            {

            }
            return dsUsers;
        }
        [Route("getcashreport")]
        [HttpPost]
        public DataSet GetCashReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsBal.GetCashReport(input);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }



        [Route("getnotreceivedreport")]
        [HttpPost]
        public DataSet GetNotReceivedReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsBal.GetNotReceivedReport(input);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        #endregion
    }
}
