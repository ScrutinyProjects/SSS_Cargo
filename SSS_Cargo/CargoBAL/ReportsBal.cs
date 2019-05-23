using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoDAL;
using CargoBE.Responses;
using System.Data;
using Newtonsoft.Json.Linq;
using CargoBE;

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



        public List<BookingReportResponse> GetBookingReport(JObject input)
        {
            List<BookingReportResponse> lstBookingReportResponse = null;

            try
            {
                DataSet ds = new DataSet();
                ds = objReportsDal.GetBookingReport(input);

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        lstBookingReportResponse = ds.Tables[0].AsEnumerable().
                                           Select(x => new BookingReportResponse
                                           {
                                               BookingId = x.Field<int>("BookingId"),
                                               GC_No = x.Field<string>("GC_No"),
                                               GC_Type = x.Field<string>("GC_Type"),
                                               FromLocation = x.Field<string>("FromLocation"),
                                               ToLocation = x.Field<string>("ToLocation"),
                                               RouteInfo = x.Field<string>("RouteInfo"),
                                               ProductType = x.Field<string>("ProductType"),
                                               Pieces = x.Field<int>("Pieces"),
                                               WeightInfo = x.Field<string>("WeightInfo"),
                                               SenderName = x.Field<string>("SenderName"),
                                               SenderMobileNumber = x.Field<string>("SenderMobileNumber"),
                                               ReceiverName = x.Field<string>("ReceiverName"),
                                               ReceiverMobileNumber = x.Field<string>("ReceiverMobileNumber"),
                                               CurrentStatus = x.Field<string>("CurrentStatus"),
                                               BillAmount = x.Field<decimal>("BillAmount"),
                                               BookedBy = x.Field<string>("BookedBy"),
                                               BookedDateTime = x.Field<DateTime>("BookedDateTime"),
                                               EncBookingId = CommonMethods.URLKeyEncrypt(Convert.ToString(x.Field<int>("BookingId")))
                                           }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstBookingReportResponse;
        }

        public bool UpdateBookingStatus(JObject input)
        {
            bool result = false;
            try
            {
                result = objReportsDal.UpdateBookingStatus(input);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public BookingPriceResponse BookingPriceDetails(int bookingId)
        {
            BookingPriceResponse objBookingPriceResponse = null;
            try
            {
                DataSet ds = objReportsDal.BookingPriceDetails(bookingId);
                if(ds != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count>0)
                {
                    objBookingPriceResponse = ds.Tables[0].AsEnumerable().
                                          Select(x => new BookingPriceResponse
                                          {
                                              BasicFrieght = x.Field<decimal>("BasicFrieght"),
                                              Hamali = x.Field<decimal>("Hamali"),
                                              SC60 = x.Field<decimal>("SC60"),
                                              ValueSC = x.Field<decimal>("ValueSC"),
                                              StatCharges = x.Field<decimal>("StatCharges"),
                                              TranshipmentCharges = x.Field<decimal>("TranshipmentCharges"),
                                              AOC = x.Field<decimal>("AOC"),
                                              CollectionCharges = x.Field<decimal>("CollectionCharges"),
                                              DeliveryCharges = x.Field<decimal>("DeliveryCharges"),
                                              WithPASS = x.Field<decimal>("WithPASS"),
                                              GST5 = x.Field<decimal>("GST5"),
                                              TotalAmount = x.Field<decimal>("TotalAmount"),
                                              DocketCharges = x.Field<decimal>("DocketCharges"),
                                              PickupCharges = x.Field<decimal>("PickupCharges"),
                                              LocationPickupCharges = x.Field<decimal>("LocationPickupCharges"),
                                              LocationDeliveryCharges = x.Field<decimal>("LocationDeliveryCharges"),
                                              DoorDeliveryCharges = x.Field<decimal>("DoorDeliveryCharges"),
                                              SubTotal = x.Field<decimal>("SubTotal"),
                                              DiscountAmount = x.Field<decimal>("DiscountAmount"),
                                              TotalAmountAfterDiscount = x.Field<decimal>("TotalAmountAfterDiscount"),
                                              RoundOffAmount = x.Field<decimal>("RoundOffAmount"),
                                              GrandTotal = x.Field<decimal>("GrandTotal"),
                                              DriverCharges = x.Field<decimal>("DriverCharges"),
                                              ToPayCharges = x.Field<decimal>("ToPayCharges")
                                          }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return objBookingPriceResponse;
        }
        public DataSet  GetNotLoadedReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsDal.GetNotLoadedReport(input);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetNotDeliveredReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsDal.GetNotDeliveredReport(input);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetUsers()
        {
            DataSet ds = null;
            try
            {
                ds = objReportsDal.GetUsers();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetCashReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsDal.GetCashReport(input);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetNotReceivedReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                ds = objReportsDal.GetNotReceivedReport(input);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        #endregion
    }
}
