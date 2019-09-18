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

            WebsitePage website = new WebsitePage
            {
                HeaderPage = new HeaderPage { 
                     Content = "GO Digital Marketing is an award-winning digital agency," +
                                                                                                           " \r\nspecializing in high scale performance marketing  and eCommerce for global brands " }
            };
            ViewBag.Website = website;
            return View();
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
            WebsitePage website = new WebsitePage
            {
                HeaderPage = new HeaderPage { Main = "About ", Content = "GO is an award-winning digital agency, specializing in high scale performance marketing and eCommerce for global brands \r\nWe offer a full-service solution starting from building eCommerce websites, Amazon stores, creating a winning digital strategy across all digital media channels, and up to data analysis and conversion rate optimization for ultimate business results.\r\n" }

            };

            ViewBag.Website = website;
            return View();
        }

        public ActionResult Solutions()
        {
            WebsitePage website = new WebsitePage
            {
                HeaderPage = new HeaderPage { Main = "Solutions ", Content = "We at GO know how to provide a complete answer at the highest professional level. From strategy, through creative cracking, to the implementation of advanced media capabilities and data systems that ultimately delivers better business results" }

            };

            ViewBag.Website = website;
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