using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Settings
{
    /// <summary>
    /// Singltone cached class helper for application settings
    /// </summary>
    public static class ApplicationSettingManager
    {
        private static WebsiteSetting _websiteSetting;

        //public static string SystemCurrencyCode => _websiteSetting.SystemCurrencyCode;

        /// <summary>
        /// <see cref="SendGrid"/> API key
        /// </summary>
        public static string SendGridApiKey => _websiteSetting.SendGridApiKey;

        /// <summary>
        /// Connetion string for azure storage account
        /// </summary>
        public static string AzureStorageConnectionString => _websiteSetting.AzureStorageConnectionString;

        public static string SQLConnectionString => _websiteSetting.SQLConnectionString;

        public static bool IsDebugMode => _websiteSetting.IsDebugMode;

        public static bool SaveDirectToDB => _websiteSetting.SaveDirectToDB;

        //public static string CellAct => _websiteSetting.GoMoblinPortal.CellAct.BaseUrl;

        static ApplicationSettingManager()
        {
            _websiteSetting = new WebsiteSetting();
        }

        internal static void Load(WebsiteSetting websiteSetting)
        {
            _websiteSetting = websiteSetting;
        }
    }
}
