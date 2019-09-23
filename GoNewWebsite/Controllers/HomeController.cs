using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities;
using Entities.Website;
using GoNewWebsite.Helpers;
using GoNewWebsite.Models;
using Newtonsoft.Json.Linq;

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


        public async Task<ActionResult> Page()
        {
            //var results = await GoWebsiteCache.GetWebsitePages();
            //WebsitePage page = results.SingleOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            //List<Section> sections = await GoWebsiteCache.GetSections();
            //ViewBag.Sections = sections;
            return View();
        }

        public async Task<ActionResult> About()
        {
            WebsitePage website = new WebsitePage
            {
                HeaderPage = new HeaderPage { Main = "About Us ", Content = "GO is an award-winning digital agency, specializing in high scale performance marketing \r\n and eCommerce for global brands. \r\n" +
                                                                         "We offer a full in-house digital solution and services. From initial strategy, building high converting eCommerce websites, mobile apps, managing Amazon stores and as well as up to Complex performance managing accounts across all digital media channels including dip level data analysis and conversion rate optimization for ultimate business results!\r\n"
                }

            };

            ViewBag.Website = website;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Submit(ContactUsViewModel modal)
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                var url = "https://api.gomoblin.co.il/api/Submited";
                JObject jObject = new JObject
                {
                    {"Id", Guid.NewGuid()},
                    {"SessionId",Guid.NewGuid() },
                    {"CampaignId", "70b3e027-660b-49dc-8403-2bfd41db2325"},
                    {"Email", modal.Email},
                    {"FullName", modal.FullName},
                    {"PhoneNumber",modal.PhoneNumber }
                };
                response = await client.PostAsync(url, new StringContent(jObject.ToString(), Encoding.UTF8, "application/json"));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(HttpStatusCode.OK, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Services()
        {
            WebsitePage website = new WebsitePage
            {
                HeaderPage = new HeaderPage { Main = "Services ", Content = "GO’s Signature advantage offers High Def Digital Marketing with a robust portfolio including: " }

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