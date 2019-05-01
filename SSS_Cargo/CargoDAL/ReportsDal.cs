using CargoBE;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoDAL
{
    public class ReportsDal
    {
        #region Members
        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["SqlCon"]);
        #endregion

        #region Methods
        public DataSet GetMyLocations(string loginId, string counterId, string requestType)
        {
            DataSet dsLocations = null;
            try
            {

                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(loginId)) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(counterId)) },
                                            new SqlParameter("@RequestType", SqlDbType.VarChar) { Value = requestType }
                                        };
                dsLocations = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetMyLocations", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsLocations;
        }
        public DataSet GetGCTypes()
        {
            DataSet dsGCTypes = null;
            try
            {
                dsGCTypes = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetGCTypes");
            }
            catch (Exception ex)
            {

            }
            return dsGCTypes;
        }
        public DataSet GetBookingStatus()
        {
            DataSet dsBookingStatus = null;
            try
            {
                dsBookingStatus = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetBookingStatus");
            }
            catch (Exception ex)
            {

            }
            return dsBookingStatus;
        }

        public DataSet GetBookingReport(JObject input)
        {
            DataSet dsBookingReport = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = Convert.ToString(input["FromDate"]) },
                                            new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = Convert.ToString(input["ToDate"]) },
                                            new SqlParameter("@LocationId", SqlDbType.Int) { Value = Convert.ToString(input["LocationId"]) },
                                            new SqlParameter("@GCTypeId", SqlDbType.Int) { Value = Convert.ToString(input["GCTypeId"]) },
                                            new SqlParameter("@BookingStatusId", SqlDbType.Int) { Value = Convert.ToString(input["BookingStatusId"]) }

                                        };
                dsBookingReport = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetBookingReport", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsBookingReport;
        }


        public bool UpdateBookingStatus(JObject input)
        {
            bool result = false;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@BookingId", SqlDbType.Int) { Value = Convert.ToInt32(input["BookingId"]) },
                                            new SqlParameter("@BookingStatusId", SqlDbType.Int) { Value = Convert.ToInt32(input["BookingStatusId"]) },
                                            new SqlParameter("@LatestRemarks", SqlDbType.VarChar) { Value = Convert.ToString(input["LatestRemarks"]) },
                                        };
                int count = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_UpdateBookingStatus", sqlparams);
                if (count > 0)
                    result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public DataSet BookingPriceDetails(int bookingId)
        {
            DataSet dsPriceDetails = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@BookingId", SqlDbType.Int) { Value = bookingId },
                                        };
                dsPriceDetails = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_BookingPriceDetails", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsPriceDetails;
        }
        
        #endregion
    }
}
