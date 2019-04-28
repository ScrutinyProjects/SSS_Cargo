using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoDAL;
using CargoBE.Responses;
using System.Data;

namespace CargoBAL
{
    public class ReportsBal
    {
        #region Members

        ReportsDal objReportsDal = new ReportsDal();

        #endregion

        #region Methods
        public List<CounterMastersResponse> GetMyLocations(string loginId, string counterId, string requestType)
        {
            List<CounterMastersResponse> lstCounters = null;

            try
            {
                DataSet ds = new DataSet();
                ds = objReportsDal.GetMyLocations(loginId, counterId, requestType);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        lstCounters = ds.Tables[0].AsEnumerable().
                                           Select(x => new CounterMastersResponse
                                           {
                                               CounterId = x.Field<int>("CounterId"),
                                               CounterName = x.Field<string>("CounterName")
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstCounters;
        }
        public List<GCTypesResponse> GetGCTypes()
        {
            List<GCTypesResponse> lstGCTypes = null;

            try
            {
                DataSet ds = new DataSet();
                ds = objReportsDal.GetGCTypes();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        lstGCTypes = ds.Tables[0].AsEnumerable().
                                           Select(x => new GCTypesResponse
                                           {
                                               GCTypeId = x.Field<int>("GCTypeId"),
                                               GCType = x.Field<string>("GCType")
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstGCTypes;
        }
        public List<BookingStatusResponse> GetBookingStatus()
        {
            List<BookingStatusResponse> lstBookingStatus = null;

            try
            {
                DataSet ds = new DataSet();
                ds = objReportsDal.GetBookingStatus();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        lstBookingStatus = ds.Tables[0].AsEnumerable().
                                           Select(x => new BookingStatusResponse
                                           {
                                               BookingStatusId = x.Field<int>("BookingStatusId"),
                                               BookingStatus = x.Field<string>("BookingStatus")
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstBookingStatus;
        }
        #endregion
    }
}
