using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TrustSystems3.BL;
using TrustSystems3.CompanyWeb.Helpers;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure.Contract;
using TrustSystems3.Repository.Infrastructure;

namespace TrustSystems3.CompanyWeb.Controllers
{
    public class BaseController: Controller
    {
        protected IUnitOfWork DbUnitOfWork { get; set; }
        protected CompanyUserRepository DbCompanyUserRepository { get; set; }
        protected CompanyRepository DbCompanyRepository { get; set; }


        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;

            // Attempt to read the culture cookie from Request
            if (cultureName == null)
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            if (RouteData.Values["culture"] as string != cultureName && cultureName != "ru")
            {

                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureName.ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }


            // Modify current thread's cultures     
            if (cultureName != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }


            return base.BeginExecuteCore(callback, state);
        }


        protected int GetCurrentCompanyId
        {
            get
            {
                if (Session[ConstantKeys.CURRENT_COMPANY_ID] == null)
                {
                    // save companyId to session
                    var companyUser = DbCompanyUserRepository.GetAll()
                        .FirstOrDefault(c => c.UserId == User.Identity.GetUserId());
                    if (companyUser != null)
                    {
                        Session[ConstantKeys.CURRENT_COMPANY_ID] = companyUser.CompanyId;
                        Session[ConstantKeys.CURRENT_USER_STATE] = companyUser.UserState;

                        return companyUser.CompanyId;
                    }
                }
                else
                {
                    return Convert.ToInt32(Session[ConstantKeys.CURRENT_COMPANY_ID]);
                }

                return 0;
            }
        }

        public BaseController()
        {
            DbUnitOfWork = new UnitOfWork();
            DbCompanyUserRepository = new CompanyUserRepository(DbUnitOfWork);
            DbCompanyRepository = new CompanyRepository(DbUnitOfWork);
        }
    }
}