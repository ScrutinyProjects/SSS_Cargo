﻿using CargoBAL;
using CargoBE.Responses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        #endregion
    }
}
