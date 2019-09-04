// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using Common.ExtentionMethods;

namespace Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RequireWwwAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly string _domain;

        public RequireWwwAttribute(string domain)
        {
            _domain = domain;
        }
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            var url = filterContext.HttpContext.Request.Url;
            var subdomain = url.GetSubdomain(_domain);
            if (string.IsNullOrWhiteSpace(subdomain))
            {
                string redirectUrl = $"https://www.{_domain}{filterContext.HttpContext.Request.RawUrl}";
                filterContext.Result = new RedirectResult(redirectUrl);
            }
        }
        
    }
}