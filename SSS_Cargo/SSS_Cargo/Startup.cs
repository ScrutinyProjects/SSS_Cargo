using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartupAttribute(typeof(CargoAPI.Startup))]
namespace CargoAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure the application for OAuth based flow
            //PublicClientId = "self";
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/token"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //    // In production mode set AllowInsecureHttp = false
            //    AllowInsecureHttp = true
            //};

            //var oauthProvider = new OAuthAuthorizationServerProvider
            //{
            //    OnGrantResourceOwnerCredentials = async context =>
            //    {
            //        if (context.UserName == "rranjan" && context.Password == "password@123")
            //        {
            //            var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            //            claimsIdentity.AddClaim(new Claim("user", context.UserName));
            //            context.Validated(claimsIdentity);
            //            return;
            //        }
            //        context.Rejected();
            //    },
            //    OnValidateClientAuthentication = async context =>
            //    {
            //        string clientId;
            //        string clientSecret;
            //        if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            //        {
            //            if (clientId == "rajeev" && clientSecret == "secretKey")
            //            {
            //                context.Validated();
            //            }
            //        }
            //    }
            //};
            //var oauthOptions = new OAuthAuthorizationServerOptions
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/accesstoken"),
            //    Provider = oauthProvider,
            //    AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(1),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(3),
            //    SystemClock = new SystemClock()

            //};
            //app.UseOAuthAuthorizationServer(oauthOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();
            //app.UseWebApi(config);
        }
    }
}