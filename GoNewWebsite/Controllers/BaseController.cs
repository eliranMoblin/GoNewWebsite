using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BLL.Cache;
using Entities;
using Entities.Portal;
using Entities.Website;
using GoNewWebsite.Helpers;

namespace GoNewWebsite.Controllers
{
    public class BaseController : Controller
    {

        public Guid SessionId
        {
            get
            {
                var session = HttpContext.Session.SessionID;
                return Guid.Parse(session);
            }
        }



        private HttpPostedFileBase[] _files = null;



        protected readonly GoWebsiteCache GoWebsiteCache;



        protected string ConnectionString =
            ConfigurationManager.ConnectionStrings["GoMoblinPortalEntities"].ConnectionString;

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                    ? Request.UserLanguages[0]
                    : // obtain it from HTTP header AcceptLanguages
                    null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        protected InternalBag Bag
        {
            get
            {

                return ViewBag.Bag;
            }

        }

        protected WebsitePage WebsitePage;
        



        public BaseController()
        {

            WebsitePage=new WebsitePage();
            GoWebsiteCache = new GoWebsiteCache();

        }
    }
}