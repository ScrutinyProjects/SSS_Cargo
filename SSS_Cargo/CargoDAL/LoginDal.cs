using CargoBE;
using CargoBE.Responses;
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
    public class LoginDal
    {
        #region Members

        SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["SqlCon"]);

        #endregion

        #region Methods

        public LoginResponse UserLogin(string username, string password)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@UserName", SqlDbType.VarChar, 100) { Value = username },
                                            new SqlParameter("@Password", SqlDbType.VarChar, 500) { Value = password }
                                        };
            SqlDataReader reader = null;
            LoginResponse objLoginResponse = new LoginResponse();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_UserLogin", sqlparams);

                while (reader.Read())
                {
                    objLoginResponse.Name = (string)reader["Name"];
                    objLoginResponse.LoginId = CommonMethods.URLKeyEncrypt(Convert.ToString((int)reader["LoginId"]));
                    objLoginResponse.LoginLogId = (long)reader["LoginLogId"];
                    objLoginResponse.UserId = CommonMethods.URLKeyEncrypt(Convert.ToString((int)reader["UserId"]));
                    objLoginResponse.RoleId = (int)reader["RoleId"];
                    objLoginResponse.RoleName = (string)reader["RoleName"];
                    objLoginResponse.StatusId = (int)reader["StatusId"];
                    objLoginResponse.StatusMessage = (string)reader["StatusMessage"];
                    objLoginResponse.CounterName = (string)reader["CounterName"];
                    objLoginResponse.CounterId = CommonMethods.URLKeyEncrypt(Convert.ToString((int)reader["CounterId"]));
                    objLoginResponse.EmailId = (string)reader["EmailId"];
                    objLoginResponse.ContactNumber = (string)reader["ContactNumber"]; 
                    objLoginResponse.OTP = (string)reader["OTP"];
                    objLoginResponse.IsOTPRequired = (bool)reader["IsOTPRequired"];
                }
            }
            catch (Exception ex)
            {
                objLoginResponse.StatusId = 0;
                objLoginResponse.StatusMessage = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objLoginResponse;
        }

        public SaveRespone ValidateLoginOTP(int loginid, string OTP)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@LoginId", SqlDbType.Int) { Value = loginid },
                                            new SqlParameter("@OTP", SqlDbType.VarChar, 6) { Value = OTP }
                                        };
            SqlDataReader reader = null;
            SaveRespone objLoginResponse = new SaveRespone();

            try
            {
                reader = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_ValidateLoginOTP", sqlparams);

                while (reader.Read())
                {
                    objLoginResponse.StatusId = (int)reader["StatusId"];
                    objLoginResponse.StatusMessage = (string)reader["StatusMessage"];
                }
            }
            catch (Exception ex)
            {
                objLoginResponse.StatusId = 0;
                objLoginResponse.StatusMessage = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objLoginResponse;
        }

        #endregion
    }
}
