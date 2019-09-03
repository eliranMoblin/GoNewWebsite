using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GoNewWebsite.Models;
using Newtonsoft.Json;

namespace GoNewWebsite.Controllers
{
    public class CareerController : Controller
    {
        // GET: Career
        public async Task<ActionResult> Index()
        {
            var jobs = await GetJobs();
            return View(jobs);
        }

        public async Task<List<Jobs>> GetJobs()
        {
            string result;
            using (WebClient client = new WebClient())
            {
                result = await client.DownloadStringTaskAsync("https://www.comeet.co/careers-api/2.0/company/A2.00E/positions?token=2AED6602AE12C2AB81014157010141014&details=true");
            }

            var jobs= JsonConvert.DeserializeObject<List<Jobs>>(result);

            return jobs;
        }

        public async Task<ActionResult> Position(string positionId)
        {
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