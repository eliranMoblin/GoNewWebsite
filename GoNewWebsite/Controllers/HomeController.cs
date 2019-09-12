using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities;
using Entities.Website;
using GoNewWebsite.Helpers;

namespace GoNewWebsite.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            //var pages = await GoWebsiteCache.GetWebsitePages();
            //var homePage = pages.SingleOrDefault(x => x.IsHomePage && x.Language == Language.Hebrew);

            WebsitePage website=new WebsitePage
            {
                HeaderPage =new HeaderPage{Main = "GO Digital Marketing ", Content = "GO is an award-winning digital agency, specializing in high scale performance marketing for global brands " } 
                
            };
            return View(website);
                //return View();
        }


        //public async Task<ActionResult> Page(string name)
        //{
        //    var results = await GoWebsiteCache.GetWebsitePages();
        //    WebsitePage page = results.SingleOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        //    List<Section> sections = await GoWebsiteCache.GetSections();
        //    ViewBag.Sections = sections;
        //    return View(page);
        //}

        public async Task<ActionResult> About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Career()
        {
            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            RouteData.Values["culture"] = culture;



            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
    }
}