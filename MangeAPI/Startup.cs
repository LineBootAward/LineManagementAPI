using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MangeAPI.App_Start;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;

[assembly: OwinStartup(typeof(MangeAPI.Startup))]

namespace MangeAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var httpConfiguration = new HttpConfiguration();
            //WebApiConfig.Register(httpConfiguration);
            //app.UseWebApi(httpConfiguration);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JWT_PasswordToken"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }
            });
        }
    }
}
