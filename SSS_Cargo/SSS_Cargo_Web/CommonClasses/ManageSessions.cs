using CargoBE.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSS_Cargo_Web
{
    public class ManageSessions
    {
        public void SaveUserSessions(LoginResponse objloginresponse)
        {
            try
            {
                HttpContext.Current.Session["SessionLogin"] = objloginresponse;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}