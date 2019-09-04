using System;
using System.Collections.Specialized;

namespace Web.Extensions
{
    public static class NameValueCollectionExtension
    {
        /// <summary>
        /// Check if collection has key
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns>true if collection contains the key, false otherwise</returns>
        public static bool HasKey(this NameValueCollection collection, string key)
        {
            var value = collection[key];

            return value != null;
        }

        /// <summary>
        /// Get <see cref="int"/> value from collection
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int? GetIntValue(this NameValueCollection collection, string key)
        {
            try
            {
                var value = collection[key];
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }
                int retVal;
                if (!int.TryParse(value, out retVal))
                {
                    return null;
                }
                return retVal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

