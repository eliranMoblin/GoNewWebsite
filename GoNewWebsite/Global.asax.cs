using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Configuration;
using DAL;
using Entities.Settings;
using log4net;
using Microsoft.ApplicationInsights.Extensibility;

namespace GoNewWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static DateTime ApplicationStartTime { get; private set; }

        protected void Application_Start()
        {
            ApplicationStartTime = DateTime.Now;

            TelemetryConfiguration.Active.DisableTelemetry = true;
            //WebsiteSettingsManager.SettingChanged += WebsiteSettingsManager_SettingChanged;
            var applicationSetting = PortalDataAccess.GetApplicationSetting();
            //WebsiteSetting applicationSetting = null;
            if (applicationSetting == null)
            {
                applicationSetting = new WebsiteSetting();
                applicationSetting.SetDefaultClient();
            }

            WebsiteSettingsManager.Load(applicationSetting);

            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //SqlDependency.Start(ApplicationSettingManager.SQLConnectionString);
        }


        //protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.Headers.Remove("Server");
        //    HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
        //    HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
        //    HttpContext.Current.Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubDomains; preload");

        //}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            WebsiteSettingsManager.Setting.WebsiteAddress = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, string.Empty);


            if (!Context.Request.IsSecureConnection)
            {
                // This is an insecure connection, so redirect to the secure version
                UriBuilder uri = new UriBuilder(Context.Request.Url);
                if (!uri.Host.Equals("localhost"))
                {
                    uri.Port = 443;
                    uri.Scheme = "https";
                    Response.Redirect(uri.ToString());

                }

            }

            //if (Request.Url.LocalPath.EndsWith("/") && !Request.Url.LocalPath.Equals("/"))
            //{
            //    UriBuilder builder = new UriBuilder(Request.Url);
            //    builder.Path = builder.Path.TrimEnd('/');
            //    Response.StatusCode = 301;

            //    Response.AddHeader("Location", builder.ToString());
            //    Response.End();
            //    return;
            //}
            //var forceWww = ConfigUtils.GetAppSettingsBool("ForceWWW", true);
            //if (!forceWww)
            //    return;

            //if (!Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
            //{
            //    UriBuilder builder = new UriBuilder(Request.Url);
            //    builder.Host = "www." + Request.Url.Host;
            //    builder.Path = builder.Path.TrimEnd('/');
            //    Response.StatusCode = 301;
            //    Response.AddHeader("Location", builder.ToString());
            //    Response.End();
            //}
        }

        //private void WebsiteSettingsManager_SettingChanged(object sender, EventArgs e)
        //{
        //    string path = HostingEnvironment.MapPath("~/content/layout.color.css");
        //    var colorCss = System.IO.File.ReadAllText(path);
        //    colorCss = colorCss.Replace("#fff", WebsiteSettingsManager.MainColor);
        //    colorCss = colorCss.Replace("#eee", WebsiteSettingsManager.SecondColor);
        //    colorCss = colorCss.Replace("#eed", WebsiteSettingsManager.MainLightColor);
        //    colorCss = colorCss.Replace(Environment.NewLine, "");
        //    string newFile = HostingEnvironment.MapPath("~/content/layout.color.min.css");
        //    System.IO.File.WriteAllText(newFile, colorCss);
        //}

        protected void Application_EndRequest()
        {
            string controller = "Error";
            string action = null;
            if (Context.Response.StatusCode == 404)
                action = "NotFound";
            else if (Context.Response.StatusCode == 401)
                action = "Unauthorized";
            else if (Context.Response.StatusCode >= 500)
                action = "ServerError";
            else if (Context.Response.StatusCode >= 400)
                action = "Error";
            if (!string.IsNullOrWhiteSpace(action))
            {
                var customError = Context.Response.Headers["customError"];
                if (!string.IsNullOrWhiteSpace(Request.QueryString["debug"])
                    || !string.IsNullOrWhiteSpace(customError))
                {
                    return;
                }

                if (Request.RequestContext.RouteData.DataTokens.ContainsKey("area"))
                {
                    var area = Request.RequestContext.RouteData.DataTokens["area"];
                    if (area.ToString().Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return;
                    }
                }
            }
        }

        protected void Application_Error()
        {
            var lastError = Server.GetLastError();
            Log.Error("Error in application", lastError);
        }

        protected void Application_End()
        {
            SqlDependency.Stop(ApplicationSettingManager.SQLConnectionString);
            Log.Warn("Application End");
            try
            {
                LogManager.ShutdownRepository();
                LogManager.Shutdown();
                Log.Warn("Application Ended");
            }
            catch (Exception exception)
            {
                Trace.TraceError($"Application_End Failed [{exception.Message}]");
                Trace.TraceError(exception.ToString());
            }
        }
    }
}
