using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Common.ExtentionMethods;

namespace Web.Extentions
{
    public static class HttpCookieCollectionExtention
    {
        private static readonly TimeSpan LifeTimeDuration = TimeSpan.FromDays(365);
        private const string DateTimeFormat = "yyyyMMddHHmmss";

        public static string GetEx(this HttpCookie httpCookie, string cookieName, string key, bool encrypt)
        {
            try
            {
                var values = httpCookie.GetValues(cookieName, encrypt);
                if (values == null)
                    return null;

                string value = null;
                if (!string.IsNullOrEmpty(values[key]))
                {
                    value = values[key];

                }
                return value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void AddUpdateOrRemove(this HttpCookie cookie, string cookieName, string key, string value, string domain, bool encrypt = true, TimeSpan? cookieExpire = null)
        {
            cookie.Domain = domain;
            cookie.Expires = DateTime.UtcNow.Add(LifeTimeDuration);

            string val = cookie.Value;
            if (encrypt)
            {
                val = Unprotect(val, cookieName);
            }
            var keyValue = val.IsNullOrWhiteSpace() ? new NameValueCollection() : HttpUtility.ParseQueryString(val);
            keyValue.UpdateValue(key, value);

            if (cookieExpire.HasValue)
            {
                var newExpire = DateTime.UtcNow.Add(cookieExpire.Value);
                keyValue.UpdateValue("Expire", newExpire.ToString(DateTimeFormat));
            }
            else
            {
                keyValue.Remove("Expire");
            }

            string values = string.Join("&", keyValue.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(keyValue[(string)a])));
            if (encrypt)
            {
                values = Protect(values, cookieName);
            }
            cookie.Value = values;
        }

        public static void AddUpdateOrRemove(this HttpCookieCollection collection, string cookieName, string key, string value, string domain, bool encrypt = true, TimeSpan? cookieExpire = null)
        {
            HttpCookie cookie = collection[cookieName];
            cookie.Domain = domain;
            cookie.Expires = DateTime.UtcNow.Add(LifeTimeDuration);

            string val = cookie.Value;
            if (encrypt)
            {
                val = Unprotect(val, cookieName);
            }
            var keyValue = val.IsNullOrWhiteSpace() ? new NameValueCollection() : HttpUtility.ParseQueryString(val);
            keyValue.UpdateValue(key, value);

            if (cookieExpire.HasValue)
            {
                keyValue.UpdateValue("Expire", DateTime.UtcNow.Add(cookieExpire.Value).ToString(DateTimeFormat));
            }
            else
            {
                keyValue.Remove("Expire");
            }
            string values = string.Join("&", keyValue.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(keyValue[a])));
            if (encrypt)
            {
                values = Protect(values, cookieName);
            }
            collection[cookieName].Value = values;
        }

        private static void UpdateValue(this NameValueCollection nvc, string key, string value)
        {
            if (nvc.AllKeys.Contains(key))
            {
                nvc[key] = value;
            }
            else
            {
                nvc.Add(key, value);
            }
        }

        public static NameValueCollection GetValues(this HttpCookie myCookie, string cookieName, bool encrypt)
        {
            var nvc = new NameValueCollection();
            try
            {
                if (myCookie == null)
                    return new NameValueCollection();
                var value = myCookie.Value;
                if (encrypt)
                {
                    value = Unprotect(myCookie.Value, cookieName);
                }
                if (value == null)
                    return nvc;

                nvc = HttpUtility.ParseQueryString(value);

                if (nvc.AllKeys.Contains("Expire"))
                {
                    var expireString = nvc["Expire"];
                    DateTime expire;
                    if (!DateTime.TryParseExact(expireString, DateTimeFormat,
                        System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                        out expire))
                    {
                        return nvc;
                    }
                    if (expire < DateTime.UtcNow)
                        return nvc;
                }
            }
            catch (Exception)
            {
            }
            return nvc;
        }

        #region Private Methods


        private static string Protect(string text, string purpose)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            byte[] stream = Encoding.UTF8.GetBytes(text);
            byte[] encodedValue = MachineKey.Protect(stream, purpose);
            return HttpServerUtility.UrlTokenEncode(encodedValue);
        }

        private static string Unprotect(string text, string purpose)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            byte[] stream = HttpServerUtility.UrlTokenDecode(text);
            byte[] decodedValue = MachineKey.Unprotect(stream, purpose);
            return Encoding.UTF8.GetString(decodedValue);
        }
        #endregion

    }
}