using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoBE.Responses
{
   public class LoginResponse
    {
        public long LoginLogId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }
        public string CounterId { get; set; }
        public string CounterName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int StatusId { get; set; }
        public string StatusMessage { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
