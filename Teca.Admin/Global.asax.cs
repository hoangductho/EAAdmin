using FX.Context;
using FX.Utils;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Teca.Admin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Config/{*pathInfo}");
            routes.MapRoute(
               "UploadFiles",
               "uploadstore/{*path}",
               new { controller = "UploadStore", action = "index" }
               );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {            
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback((s, ce, ch, ssl) => true);
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("~/") + "config/logging.config"));
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Bootstrapper.InitializeContainer();            
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if (lastError is HttpRequestValidationException || lastError is ArgumentException)
            {
                Response.Redirect("/Home/PotentiallyError");
            }
            if(lastError is UnauthorizedAccessException)
                Response.Redirect("/Home/Unauthorized");
        }

        private void GetCurrentSite(HttpContext context)
        {
            IFXContext _FXcontext = FXContext.Current;
            try
            {
                string siteUrl = UrlUtil.GetSiteUrl();
                _FXcontext.PhysicalSiteDataDirectory = ConfigurationSettings.AppSettings.Get("PhysicalSiteDataDirectory");
                _FXcontext.SetSite(siteUrl);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            GetCurrentSite(HttpContext.Current);
        }

        /**********************************Multi Language***************************************/
        public void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //It's important to check whether session object is ready --- session đã sẵn sàng 
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                if (ci == null) // ==> truy cập lần đầu tiên 
                {   // chưa có thông tin culture, cho nó là mặc định (Tiếng Việt)
                    string langName = "vi";
                    //Try to get values from Accept lang HTTP header    --- chưa hiểu lắm
                    //if (HttpContext.Current.Request.UserLanguages != null &&
                    //    HttpContext.Current.Request.UserLanguages.Length != 0)
                    //{
                    //    //Gets accepted list 
                    //    langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    //}
                    ci = new CultureInfo(langName);
                    this.Session["Culture"] = ci;
                }
                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
        /************************hết multi Language************************************/

        
    }
}