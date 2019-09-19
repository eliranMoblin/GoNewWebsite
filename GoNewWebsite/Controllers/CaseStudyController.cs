using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities.Website;

namespace GoNewWebsite.Controllers
{
    public class CaseStudyController : BaseController
    {
        WebsitePage website = new WebsitePage
        {
            HeaderPage = new HeaderPage
            {
                Main = "Case Study"
            }
        };
        // GET: CaseStudy
        public ActionResult SuperPharmEn()
        {
            ViewBag.Website = website;

            return View();
        }

        public ActionResult CaesarstoneEN()
        {
            
            ViewBag.Website = website;

            return View();
        }

        public ActionResult ScanMarker()
        {
            ViewBag.Website = website;
            return View();
        }

        public ActionResult MinistryEN()
        {
            
            ViewBag.Website = website;

            return View();
        }
    }
}