using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using MangeAPI.App_Start;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Net.Http;
using System.Web.Hosting;

[assembly: OwinStartup(typeof(MangeAPI.Startup))]

namespace MangeAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
            var httpConfig = new HttpConfiguration();
            //WebApiConfig.Register(httpConfig);
            //app.UseWebApi(httpConfig);

            //app.Use<GlobalExceptionMiddleware>();
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

    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        public GlobalExceptionMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }

        //Controllers例外處理
        //var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        //{
        //    Content = new StringContent(string.Format("No product with ID = 0")),
        //        ReasonPhrase = "Product ID Not Found"
        //    };
        //    throw new HttpResponseException(resp);
        //}
}
