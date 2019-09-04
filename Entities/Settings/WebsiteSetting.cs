using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.System;
using Newtonsoft.Json;

namespace Entities.Settings
{
    /// <summary>
    /// Singltone cached class helper for application settings
    /// </summary>
    public class WebsiteSetting : IDocument
    {
        public Document Document { get; set; }

        public string SQLConnectionString { get; set; }

        public bool IsDebugMode { get; set; }

        public bool SaveDirectToDB { get; set; }

        public WebsiteSetting Debug { get; set; }

        /// <summary>
        /// Website display name
        /// </summary>
        public string WebsiteName { get; set; }

        /// <summary>
        /// Website Url
        /// </summary>
        public string WebsiteAddress { get; set; }

        public string AMPMandatoryScript { get; set; }


        /// <summary>
        /// <see cref="SendGrid"/> API key
        /// </summary>
        public string SendGridApiKey { get; set; }

        /// <summary>
        /// <see cref="Recaptcah"/> publick key
        /// </summary>
        public string RecaptchaPublicKey { get; set; }

        /// <summary>
        /// <see cref="Recaptcah"/> Private key
        /// </summary>
        public string RecaptchaPrivateKey { get; set; }

        /// <summary>
        /// <see cref="Recaptcah"/> API version
        /// </summary>
        public string RecaptchaApiVersion { get; set; }

        /// <summary>
        /// Connetion string for azure storage account
        /// </summary>
        public string AzureStorageConnectionString { get; set; }


        /// <summary>
        /// path we block when user request. mostly wordpress robots
        /// </summary>
        public string BlockedPath { get; set; }

        //public string JsonConfiguration { get; set; }
        public string WhitelistIps { get; set; }

        public GoMoblinPortal GoMoblinPortal { get; set; }

        public string reCapatchPublicKey { get; set; }

        public string reCapatchPrivateKey { get; set; }
        public bool reCapatchEnabled { get; set; }

        //[JsonIgnore]
        //public string Zubi { get; set; }

        public WebsiteSetting()
        {
            GoMoblinPortal = new GoMoblinPortal();
        }

        public void SetDefaultClient()
        {
            //LayoutStyle = "<link href='/Content/mdl-v1.1.2/material.min.css' rel='stylesheet' />";
            //LayoutDeferredStyle = "<link href='/Content/layout.min.css?version=2' rel='stylesheet' />" +
            //                      "<link rel='stylesheet' href='https://fonts.googleapis.com/icon?family=Material+Icons'> " +
            //                      "<link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Heebo:300,400,700'> " +
            //                      "<link rel='stylesheet' type='text/css' href='//cdnjs.cloudflare.com/ajax/libs/cookieconsent2/3.0.3/cookieconsent.min.css' />";

            //LayoutScript = "<script async defer type='text/javascript' src='/Scripts/jquery-3.3.1.min.js'></script>" +
            //               "<script async defer type='text/javascript' src='/Scripts/Material/material.min.js'></script>" +
            //               "<script async defer type='text/javascript' src='/Scripts/lozad/lozad.min.js'></script>" +
            //               "<script async defer type='text/javascript' src='/Scripts/app/modernizr-custom.min.js'></script>" +
            //               "<script async defer type='text/javascript' src='/Scripts/app/app.min.js?version=2'></script>";

            //RobotsTxt = "user-agent: *    " + "Allow: /                                " +
            //            "Disallow: / Admin /                     " + "Disallow: / Affiliate /                 " +
            //            "Disallow: / Advertiser /                " + "Disallow: */goto                        " +
            //            "Disallow: / Redirect /                  " + "Disallow: / UpdateDetails / ";

            WebsiteName = "Unknown";



            //SystemTheme = Theme.NoTheme;
            BlockedPath = "/wp-admin;/wp-admin.php;/wp-login.php";

        }

    }

    public class GoMoblinPortal
    {
        //public string AmazonSecretKey { get; set; }

        //public string AmazonAccessKey { get; set; }

        public string AzureStorageKey { get; set; }

        //public SMSProvider[] SMSProvider { get; set; }

        //public CellAct CellAct { get; set; }

        //public ClickATell ClickATell { get; set; }

        //public InforU InforU { get; set; }
    }

    //public class CellAct : Provider
    //{
        
    //}

    //public class ClickATell : Provider
    //{
        
    //}

    //public class InforU : Provider
    //{
    //}

}
