using CargoBE.Responses;
using CargoDAL;
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
