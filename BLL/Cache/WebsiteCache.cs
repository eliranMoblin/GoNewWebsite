using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common;
using log4net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Hosting;
using DAL;
using Entities.Settings;
using Entities.System;
using Country = Entities.System.Country;
using Language = Shared.Entities.System.Language;


namespace BLL.Cache
{
    public class WebsiteCache : Singleton<WebsiteCache>, IInit
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);



        public List<Country> Countries { get; private set; }

        public List<Language> Languages { get; private set; }

        public async Task InitAsync()
        {

        }

        public void Init()
        {
            Countries = new List<Country>();
            Languages = new List<Language>();
            LoadData();
        }


        #region Private Methods

        private void LoadData()
        {
            Stopwatch allProcessStopwatch = Stopwatch.StartNew();

            Log.Info("LoadData Started");
            HostingEnvironment.QueueBackgroundWorkItem(async (x) => await LoadGeoData());

            Log.Info($"LoadData Ended Took {allProcessStopwatch.Elapsed}");
        }
        private async Task LoadGeoData()
        {
            try
            {
                GoWebsiteCache websiteCache = new GoWebsiteCache();
                Stopwatch stopwatch = Stopwatch.StartNew();

                HostingEnvironment.QueueBackgroundWorkItem(async (x) =>
                {
                    var settings = PortalDataAccess.GetApplicationSetting();
                    if (settings == null)
                    {
                        settings = new WebsiteSetting();
                        settings.SetDefaultClient();
                    }

                    WebsiteSettingsManager.Load(settings);
                });

                Log.Info($"Wating for all tasks to end {stopwatch.Elapsed}");

                Log.Info($"all tasks to end {stopwatch.Elapsed}");

            }
            catch (Exception ex)
            {
                Log.Error("Failed to load data");
                Log.Error(ex.ToString());
                throw;
            }
        }

        #endregion
    }
}
