using MangeAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MangeAPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            //AutofacConfig.Bootstrapper();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //一律輸出json，並且格式化
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        }

        void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;
            //context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (context.Request.HttpMethod == "OPTIONS")
            {
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.AddHeader("Access-Control-Allow-Headers", @"Authorization,Content-Type, 
        authorization,content-Type");
                context.Response.AddHeader("Access-Control-Allow-Methods", "POST,GET,OPTIONS");
                HttpContext.Current.Response.End();
            }
        }
    }
}