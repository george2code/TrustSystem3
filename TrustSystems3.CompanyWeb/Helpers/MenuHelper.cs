using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using TrustSystems3.BL.Utils;

namespace TrustSystems3.CompanyWeb.Helpers
{
    public static class MenuHelper
    {
        public static MvcHtmlString MenuLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName
            )
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (actionName == StringUtils.UppercaseFirst(currentAction) && 
                controllerName == StringUtils.UppercaseFirst(currentController))
            {
                return htmlHelper.ActionLink(
                    linkText,
                    actionName,
                    controllerName,
                    null,
                    new
                    {
                        @class = "active"
                    });
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }

        public static MvcHtmlString MenuLinkController(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName
            )
        {
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (controllerName == StringUtils.UppercaseFirst(currentController))
            {
                return htmlHelper.ActionLink(
                    linkText,
                    actionName,
                    controllerName,
                    null,
                    new
                    {
                        @class = "active"
                    });
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }

        public static String DashboardSidebarMenuLink(
            this HtmlHelper htmlHelper,
            string linkText,
            string actionName,
            string controllerName
            )
        {
            StringBuilder sb = new StringBuilder();

            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (actionName == StringUtils.UppercaseFirst(currentAction) &&
                controllerName == StringUtils.UppercaseFirst(currentController))
            {
                sb.Append("<li role=\"presentation\" class=\"active\">");
                sb.AppendFormat("<a href=\"{0}\"><span class=\"glyphicon glyphicon-home\"></span> {1}</a>",
                    htmlHelper.Action(actionName, controllerName),
                    linkText);
                sb.Append("</li>");

            }
            else
            {
                sb.Append("<li role=\"presentation\">");
                sb.AppendFormat("<a href=\"{0}\"><span class=\"glyphicon glyphicon-home\"></span> {1}</a>",
                    htmlHelper.Action(actionName, controllerName),
                    linkText);
                sb.Append("</li>");
            }

            return sb.ToString();
        }
    }
}