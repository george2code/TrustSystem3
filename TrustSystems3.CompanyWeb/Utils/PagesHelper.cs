using System;

namespace TrustSystems3.CompanyWeb.Utils
{
    public static class PagesHelper
    {
        public static string GetBusinessTitle(string title)
        {
            var baseTitle = "TrustBusiness";
            return (String.IsNullOrEmpty(title)) ? 
                title : string.Format("{0} | {1}", title, baseTitle);

        }


    }
}