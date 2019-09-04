using System;
using Common.ExtensionMethods;
using Newtonsoft.Json;

namespace Entities.Settings
{
    /// <summary>
    /// Singltone cached class helper for application settings
    /// </summary>
    public static class WebsiteSettingsManager
    {
        private static WebsiteSetting _websiteSetting;
        private static GoMoblinPortal _goMoblinPortal;
        public static event EventHandler SettingChanged;
        public static WebsiteSetting Setting => _websiteSetting;


        public static string reCapatchPublicKey => _websiteSetting.reCapatchPublicKey;

        public static string reCapatchPrivateKey => _websiteSetting.reCapatchPrivateKey;

        public static bool reCapatchEnabled => _websiteSetting.reCapatchEnabled;

        /// <summary>
        /// Website display name
        /// </summary>
        public static string WebsiteName => _websiteSetting.WebsiteName;


        public static string WebsiteAddress => _websiteSetting.WebsiteAddress;

        public static string AMPMandatoryScript => _websiteSetting.AMPMandatoryScript;


        /// <summary>
        /// <see cref="SendGrid"/> API key
        /// </summary>
        //public static string SendGridApiKey => _applicationSetting.ScriptFolder;

        /// <summary>
        /// <see cref="Recaptcah"/> publick key
        /// </summary>
        public static string RecaptchaPublicKey => _websiteSetting.RecaptchaPublicKey;


        /// <summary>
        /// <see cref="Recaptcah"/> Private key
        /// </summary>
        public static string RecaptchaPrivateKey => _websiteSetting.RecaptchaPrivateKey;


        /// <summary>
        /// <see cref="Recaptcah"/> API version
        /// </summary>
        public static string RecaptchaApiVersion => _websiteSetting.RecaptchaApiVersion;

   
        /// <summary>
        /// Connetion string for azure storage account
        /// </summary>
        //public static string AzureStorageConnectionString => _applicationSetting.AzureStorageConnectionString;

        /// <summary>
        /// user browser session duration
        /// </summary>
        public static TimeSpan SessionDuration { get; set; }



        public static string AzureStorageKey => _websiteSetting.GoMoblinPortal.AzureStorageKey;

        public static GoMoblinPortal GoMoblinPortal => _goMoblinPortal;



        static WebsiteSettingsManager()
        {
            _websiteSetting = new WebsiteSetting();
        }


        public static void Load(WebsiteSetting websiteSetting)
        {
            WebsiteSetting settings;
            if (websiteSetting.IsDebugMode && websiteSetting.Debug != null)
            {
                settings = websiteSetting.Debug;
                settings.IsDebugMode = true;
            }
            else
            {
                settings = websiteSetting;
            }

            ApplicationSettingManager.Load(settings);

            _websiteSetting = settings;
            var rountyInfoJson = JsonConvert.SerializeObject(websiteSetting.GoMoblinPortal);
            _goMoblinPortal = JsonConvert.DeserializeObject<GoMoblinPortal>(rountyInfoJson);




         

            var type = _goMoblinPortal.GetType();
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    var propValue = propertyInfo.GetValue(_goMoblinPortal);
                    if (propValue == null)
                        continue;
                    var propStringVal = propValue.ToString();
                    //propStringVal = propStringVal.Replace("*|affiliateid|*", GoMoblinPortal.AffiliateId.ToString(), StringComparison.CurrentCultureIgnoreCase);
                    //propStringVal = propStringVal.Replace("*|apikey|*", GoMoblinPortal.APIKey, StringComparison.CurrentCultureIgnoreCase);
                    propertyInfo.SetValue(_goMoblinPortal, propStringVal);
                }
            }
            OnSettingChanged();
        }

        private static void OnSettingChanged()
        {
            SettingChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
