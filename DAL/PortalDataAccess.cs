using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Entities;
using Entities.System;
using Entities.Settings;
using Newtonsoft.Json;
using Web.Entities;

namespace DAL
{
    public class PortalDataAccess
    {
        #region Data Member
        private readonly string _connectionString;
        #endregion


        private PortalDataContext Context => new PortalDataContext(_connectionString);

        public PortalDataAccess(string connectionString)
        {
            _connectionString = connectionString;
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public static WebsiteSetting GetApplicationSetting()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["SettingsConnectionString"];
            //if (connectionString==null)
            //{
            //    connectionString.ConnectionString =
            //        ;
            //}
            using (var context = new PortalDataContext("Server=tcp:gomoblinportaldb.database.windows.net,1433;Initial Catalog=GoMoblinPortal;Persist Security Info=False;User ID=MoblinSA;Password=Moblin1@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                Backend_Settings_GetResult settings = context.Backend_Settings_Get().FirstOrDefault();
                WebsiteSetting setting;
                if (settings == null)
                    setting = GetApplicationSettingFromFile();
                else
                    setting = JsonConvert.DeserializeObject<WebsiteSetting>(settings.Json);

                return setting;
            }
        }

        private static WebsiteSetting GetApplicationSettingFromFile()
        {
            var configFilePath = HostingEnvironment.MapPath("~/websetting_new.json");
            if (!File.Exists(configFilePath))
                configFilePath = HostingEnvironment.MapPath("~/websetting.json");
            var jsonSetting = File.ReadAllText(configFilePath);

            var setting = JsonConvert.DeserializeObject<WebsiteSetting>(jsonSetting);
            return setting;

        }

        //internal void InsertChangesHistory(string name, string objectId, string json, Guid? userId, byte changeType, long tstamp)
        //{
        //    Task.Run(() =>
        //    {

        //        using (var context = Context)
        //        {
        //            context.Backend_InsertChangesHistory(name, objectId, json, userId, changeType, tstamp);
        //        }
        //    });
        //}



        public static async Task InsertHttpRequest(HttpRequestInfo httpRequest)
        {
            await Task.Run(() =>
            {
                using (var context = new PortalDataContext(ApplicationSettingManager.SQLConnectionString))
                {
                    context.InsertHttpRequest(sessionId: httpRequest.SessionId, userHostAddress: httpRequest.UserHostAddress, userId: httpRequest.UserId, device: httpRequest.Device, urlReferrer: httpRequest.UrlReferrer,
                        requestUrl: httpRequest.RequestUrl, machineId: httpRequest.MachineId, cookies: httpRequest.Cookies, queryString: httpRequest.Querystring, isNewSeession: httpRequest.isNewSession);
                }
            });
        }


        public static IList<ResourceEntry> GetResources()
        {
            List<WebSite_GetResources_ValueResult> resources = null;

            IList<ResourceEntry> list =  new List<ResourceEntry>();
            using (var context = new PortalDataContext("Server=tcp:gomoblinportaldb.database.windows.net,1433;Initial Catalog=GoMoblinPortal;Persist Security Info=False;User ID=MoblinSA;Password=Moblin1@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                resources = context.WebSite_GetResources_Value().ToList();
            }

            foreach (var resource in resources)
            {
                list.Add(new ResourceEntry
                {
                    Culture = resource.Culture,
                    Name = resource.Name,
                    Value = resource.Value
                });
            }
            return list;
        }

        public static ResourceEntry GetResource(string name, string culture)
        {
            ResourceEntry resources = null;
            string cultureOut = null;
            string valueOut = null;
            string nameOut = null;

            using (var context = new PortalDataContext("Server=tcp:gomoblinportaldb.database.windows.net,1433;Initial Catalog=GoMoblinPortal;Persist Security Info=False;User ID=MoblinSA;Password=Moblin1@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                context.WebSite_GetResource_Value(culture: culture, name: name, nameOut: ref nameOut, cultureOut: ref cultureOut, valueOut: ref valueOut);
            }
            resources = new ResourceEntry
            {
                Culture = cultureOut,
                Name = nameOut,
                Value = valueOut
            };

            return resources;
        }


    }
}
