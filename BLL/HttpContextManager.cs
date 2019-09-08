using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BLL;
using Common;
using Common.ExtensionMethods;
using Common.ExtentionMethods;
using Core;
using Entities;
using Entities.Settings;

namespace BLL
{
    public class HttpContextManager : Singleton<HttpContextManager>, IInit
    {

        #region Data Member

        private readonly TimeSpan _lifeTimeDuration = TimeSpan.FromDays(365);


        private TimeSpan _sessionDuration = WebsiteSettingsManager.SessionDuration;

        public string LanguageCode => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public SessionWebCookie Session { get; private set; }

        public UserWebCookie User { get; private set; }



        #endregion


        public void Init()
        {
            if (_sessionDuration == TimeSpan.Zero)
                _sessionDuration = TimeSpan.FromMinutes(20);

            Session = new SessionWebCookie(SystemCoockies.SessionInfo, null, true, _sessionDuration);

            User = new UserWebCookie(SystemCoockies.UserInfo, null, true, _lifeTimeDuration);

        }

        public async Task InitAsync()
        {

        }

        public void ExtendCookies()
        {
            Session.UpdateCookieExpirationDate(true);
            User.UpdateCookieExpirationDate(false);
        }


        public string CountryCode
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Session.SelectedCountryCode))
                    return Session.SelectedCountryCode;

                if (!string.IsNullOrWhiteSpace(Session.UserCountryCode) && !Session.UserCountryCode.Equals("xx", StringComparison.CurrentCultureIgnoreCase))
                    return Session.UserCountryCode;

                if (!string.IsNullOrWhiteSpace(Session.IpCountryCode))
                    return Session.IpCountryCode;


#if DEBUG
                //        return "IL";
#endif
                return "XX";
            }
        }

        public Dictionary<string, string> GetAllCoockies()
        {
            //IpCountryCode
            Dictionary<string, string> coockies = new Dictionary<string, string>();

            foreach (var cookie in User.GetAllCookies())
            {

                if (cookie.Value.IsNullOrWhiteSpace())
                    continue;
                coockies.Add(cookie.Key, cookie.Value);
            }
            foreach (var cookie in Session.GetAllCookies())
            {
                if (cookie.Value.IsNullOrWhiteSpace())
                    continue;
                coockies.Add(cookie.Key, cookie.Value);
            }

            return coockies;
        }

    }
}
