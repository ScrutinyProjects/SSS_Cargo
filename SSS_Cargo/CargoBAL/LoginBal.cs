using CargoBE;
using CargoBE.Responses;
using CargoDAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBAL
{
    public class LoginBal
    {
        #region Members

        LoginDal objLoginDal = new LoginDal();

        #endregion

        #region Methods

        public LoginResponse UserLogin(string username, string password)
        {
            LoginResponse objLoginResponse = new LoginResponse();

            try
            {
                objLoginResponse = objLoginDal.UserLogin(username, password);
                objLoginResponse.OTP = string.Empty;
            }
            catch (Exception ex)
            {
                objLoginResponse.StatusId = 0;
                objLoginResponse.StatusMessage = ex.Message;
            }
            return objLoginResponse;
        }

        public SaveRespone ValidateLoginOTP(JObject input) 
        {
            SaveRespone objLoginResponse = new SaveRespone();

            try
            {
                int loginid = Convert.ToInt32(CommonMethods.URLKeyDecrypt(Convert.ToString(input["LoginId"])));
                string OTP = Convert.ToString(input["OTP"]);

                objLoginResponse = objLoginDal.ValidateLoginOTP(loginid, OTP);
            }
            catch (Exception ex)
            {
                objLoginResponse.StatusId = 0;
                objLoginResponse.StatusMessage = ex.Message;
            }
            return objLoginResponse;
        }

        #endregion
    }
}
