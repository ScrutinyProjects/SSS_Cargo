using CargoBAL;
using CargoBE.Responses;
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
        #endregion
    }
}
