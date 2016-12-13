namespace TrustSystems3.BL.Email
{
    public class TemplateFilter
    {
        public static string Proceed(string text, string name, string link, string domain, string order)
        {
            if (!string.IsNullOrEmpty(name))
                text = text.Replace("[Name]", name);
            if (!string.IsNullOrEmpty(name))
                text = text.Replace("[Link]", link);
            if (!string.IsNullOrEmpty(name))
                text = text.Replace("[DomainName]", domain);
            if (!string.IsNullOrEmpty(name))
                text = text.Replace("[Order]", order);

            return text;
        }
    }
}