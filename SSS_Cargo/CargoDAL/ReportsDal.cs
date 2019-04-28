using CargoBE;
using Microsoft.ApplicationBlocks.Data;
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
                                            new SqlParameter("@RequestType", SqlDbType.Date) { Value = requestType }
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
        #endregion
    }
}
