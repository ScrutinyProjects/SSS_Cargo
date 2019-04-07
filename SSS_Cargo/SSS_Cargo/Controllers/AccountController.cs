using CargoBAL;
using CargoBE;
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
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        #region Members

        LoginBal objLoginBal = new LoginBal();

        #endregion

        #region APIs
        
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public LoginResponse UserLogin(JObject input)
        {
            LoginResponse objresponse = new LoginResponse();
            try
            {
                string username = Convert.ToString(input["Username"]);
                string password = CommonMethods.Encrypt(Convert.ToString(input["Password"]));

                objresponse = objLoginBal.UserLogin(username, password);
            }
            catch (Exception ex)
            {
                objresponse.StatusId = 0;
                objresponse.StatusMessage = ex.Message;
            }
            return objresponse;
        }

        #endregion
    }
}
