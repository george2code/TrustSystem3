using System;
using System.Security.Policy;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace TrustSystems3.ClientWeb.Utils
{
    public static class PagesHelper
    {
        public static string GetBusinessTitle(string title)
        {
            var baseTitle = "TrustSystems";
            return (String.IsNullOrEmpty(title)) ?
                title : string.Format("{0} | {1}", title, baseTitle);

        }
    }
}