using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities.Website;
using GoNewWebsite.Models;
using Newtonsoft.Json;

namespace GoNewWebsite.Controllers
{
    public class CareerController : BaseController
    {
        // GET: Career
        WebsitePage website = new WebsitePage
        {
            HeaderPage = new HeaderPage
            {
                Main = "Careers"
            }
        };
        public ActionResult Index()
        {
            var jobs =GetJobs();
            ViewBag.Website = website;
            //            WebsitePage.HeaderPage.Main = "Career";
            return View(jobs);
        }

        public ActionResult GetJobsForHomePage()
        {
            var jobs = GetJobs();
            var newJobs = jobs.Where(x => !string.IsNullOrWhiteSpace(x.Details[0].Value) ).Take(6).ToList();
            return PartialView(newJobs);
        }


        public List<Jobs> GetJobs()
        {
            string result;
            using (WebClient client = new WebClient())
            {
                result =  client.DownloadString("https://www.comeet.co/careers-api/2.0/company/A2.00E/positions?token=2AED6602AE12C2AB81014157010141014&details=true");
            }

            var jobs= JsonConvert.DeserializeObject<List<Jobs>>(result);

            return jobs;
        }

        public async Task<ActionResult> Position(string positionId)
        {
      
            ViewBag.Website = website;
            string result;
            using (WebClient client = new WebClient())
            {
                result = await client.DownloadStringTaskAsync($"https://www.comeet.co/careers-api/2.0/company/A2.00E/positions/{positionId}?token=2AED6602AE12C2AB81014157010141014&details=true");
            }

            var job = JsonConvert.DeserializeObject<Position>(result);

            return View(job);
        }
    }
}