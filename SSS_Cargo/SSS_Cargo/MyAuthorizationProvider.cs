using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Configuration;
using Microsoft.Owin.Security;
using System.Data;
using CargoBAL;
using CargoBE;
using CargoBE.Responses;

namespace SSS_Cargo
{
    public class MyAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        LoginBal objLoginBal = new LoginBal();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var form = await context.Request.ReadFormAsync();

                string username = context.UserName;
                string password = CommonMethods.Encrypt(context.Password);

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                
                LoginResponse objLoginResponse = new LoginResponse();
                objLoginResponse = objLoginBal.UserLogin(username, password);

                if (objLoginResponse.StatusId == 1)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, objLoginResponse.RoleName));
                    identity.AddClaim(new Claim("username", username));
                    identity.AddClaim(new Claim(ClaimTypes.Name, objLoginResponse.Name));

                    AuthenticationProperties properties = CreateUserProperties(objLoginResponse);
                    AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
                    context.Validated(ticket);
                }
                else
                {
                    context.SetError("invalid_grant", objLoginResponse.StatusMessage);
                    return;
                }
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
                return;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateUserProperties(LoginResponse objLoginResponse)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "Name", objLoginResponse.Name },
                { "CounterId", objLoginResponse.CounterId },
                { "CounterName", objLoginResponse.CounterName },
                { "RoleId", objLoginResponse.RoleId.ToString() },
                { "RoleName", objLoginResponse.RoleName },
                { "EmailId", objLoginResponse.EmailId },
                { "ContactNumber", objLoginResponse.ContactNumber },
                { "LoginId", objLoginResponse.LoginId },
                { "LoginLogId", objLoginResponse.LoginLogId.ToString() },
                { "StatusId", objLoginResponse.StatusId.ToString() },
                { "StatusMessage", objLoginResponse.StatusMessage },
                { "UserId", objLoginResponse.UserId }
            };
            return new AuthenticationProperties(data);
        }
    }
}