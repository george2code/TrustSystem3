using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace TrustSystems3.ClientWeb.Utils
{
    public class UrlClientHelper
    {
        public static string GetUserUrl(string userId)
        {
            return String.Format("/users/{0}", userId);
        }

        public static string GetCompanyReviews(string companySlug)
        {
            return String.Format("/review/{0}", companySlug);
        }
    }
}