using System.Web.UI;

namespace TrustSystems3.ClientWeb.Utils
{
    public class TagBuilderHelper
    {
        public static string GetUserLink(AspNetUsers user)
        {
            return (user != null)
                ? string.Format("<a href=\"{0}\">{1}</a>", UrlClientHelper.GetUserUrl(user.Id), user.UserName)
                : string.Empty;
        }

        public static string GetCompanyLink(Companies company)
        {
            return string.Format("<a href=\"{0}\">{1}</a>",
                UrlClientHelper.GetCompanyReviews(company.Slug),
                company.Name
                );
        }
    }
}