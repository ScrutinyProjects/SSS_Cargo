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
                                            new SqlParameter("@GCTypes", SqlDbType.VarChar) { Value = Convert.ToString(input["GCTypes"]) },
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

        public DataSet GetNotLoadedReport(JObject input)
        {
            DataSet dsNotLoadedReport = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = Convert.ToString(input["FromDate"]) },
                                            new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = Convert.ToString(input["ToDate"]) },
                                            new SqlParameter("@LocationId", SqlDbType.Int) { Value = Convert.ToString(input["LocationId"]) }

                                        };
                dsNotLoadedReport = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetNotLoadedReport", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsNotLoadedReport;
        }

        public DataSet GetNotDeliveredReport(JObject input)
        {
            DataSet dsNotDeliveredReport = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = Convert.ToString(input["FromDate"]) },
                                            new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = Convert.ToString(input["ToDate"]) }
                                        };
                dsNotDeliveredReport = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetNotDeliveredReport", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsNotDeliveredReport;
        }

        public DataSet GetUsers()
        {
            DataSet dsUsers = null;
            try
            {
                dsUsers = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetUsers");
            }
            catch (Exception ex)
            {

            }
            return dsUsers;
        }

        public DataSet GetUserCashReport(JObject input)
        {
            DataSet dsUserCashReport = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@TransactionDate", SqlDbType.DateTime) { Value = Convert.ToString(input["TransactionDate"]) },
                                            new SqlParameter("@LocationId", SqlDbType.Int) { Value = Convert.ToString(input["LocationId"]) },
                                            new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToString(input["UserId"]) }
                                        };
                dsUserCashReport = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "Usp_getusercashreport", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsUserCashReport;
        }


        public DataSet GetNotReceivedReport(JObject input)
        {
            DataSet dsNotReceivedReport = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = Convert.ToString(input["FromDate"]) },
                                            new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = Convert.ToString(input["ToDate"]) }
                                        };
                dsNotReceivedReport = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetNotReceivedReport", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsNotReceivedReport;
        }


        public DataSet GetReceivingTypes()
        {
            DataSet ds = null;
            try
            {
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetReceivingTypes");
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetCashConsolidationReport(JObject input)
        {
            DataSet dsCashConsolidationReport = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@TransactionDate", SqlDbType.DateTime) { Value = Convert.ToString(input["TransactionDate"]) },
                                            new SqlParameter("@LocationId", SqlDbType.Int) { Value = Convert.ToString(input["LocationId"]) }
                                        };
                dsCashConsolidationReport = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetCashConsolidationReport", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsCashConsolidationReport;
        }

        public DataSet GetCashHandoverReport(JObject input)
        {
            DataSet dsCashHandoverReport = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@TransactionDate", SqlDbType.DateTime) { Value = Convert.ToString(input["TransactionDate"]) },
                                            new SqlParameter("@LocationId", SqlDbType.Int) { Value = Convert.ToString(input["LocationId"]) }
                                        };
                dsCashHandoverReport = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetCashHandoverReport", sqlparams);
            }
            catch (Exception ex)
            {

            }
            return dsCashHandoverReport;
        }
        public bool UpdateCashHandover(JObject input)
        {
            bool result = true;
            try
            {
                foreach (var item in input["CashData"])
                {
                    SqlParameter[] sqlparams = {
                                            new SqlParameter("@TransactionDate", SqlDbType.DateTime) { Value = Convert.ToDateTime(input["TransactionDate"]) },
                                            new SqlParameter("@LocationId", SqlDbType.Int) { Value = Convert.ToInt32(Convert.ToString(input["LocationId"])) },
                                            new SqlParameter("@UserId", SqlDbType.Int) { Value = Convert.ToInt32(Convert.ToString(item["UserId"])) },
                                            new SqlParameter("@PaidAmount", SqlDbType.Decimal) { Value = Convert.ToDecimal(Convert.ToDouble(item["PaidAmount"])) }
                                        };
                    SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_UpdateCashHandover", sqlparams);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public DataSet GetReceivingReport(JObject input)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] sqlparams = {
                                            new SqlParameter("@LoginUserId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"]))) },
                                            new SqlParameter("@CounterId", SqlDbType.Int) { Value = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["CounterId"]))) },
                                            new SqlParameter("@FromDate", SqlDbType.DateTime) { Value = Convert.ToString(input["FromDate"]) },
                                            new SqlParameter("@ToDate", SqlDbType.DateTime) { Value = Convert.ToString(input["ToDate"]) },
                                            new SqlParameter("@LocationId", SqlDbType.Int) { Value = Convert.ToString(input["LocationId"]) },
                                            new SqlParameter("@GCTypes", SqlDbType.VarChar) { Value = Convert.ToString(input["GCTypes"]) },
                                            new SqlParameter("@ReceivingTypes", SqlDbType.VarChar) { Value = Convert.ToString(input["ReceivingTypes"]) }

                                        };
                ds = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "USP_GetReceivingReport", sqlparams);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        #endregion
    }
}
