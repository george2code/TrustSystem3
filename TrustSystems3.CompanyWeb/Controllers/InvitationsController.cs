using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrustSystems3.Repository;

namespace TrustSystems3.CompanyWeb.Controllers
{
    public class InvitationsController : BaseController
    {
        private InvitationRepository _invitationRepository = null;

        public InvitationsController()
        {
            _invitationRepository = new InvitationRepository(base.DbUnitOfWork);
        }


        // GET: Invitations
        public ActionResult History()
        {
            List<Invitation> invitationList = new List<Invitation>();
            invitationList = _invitationRepository.GetAll().Where(c => c.CompaniesId == GetCurrentCompanyId).ToList();
            
            return View(invitationList);
        }


        public ActionResult InviteCustomers()
        {
            return View();
        }
        
        public ActionResult Afs()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }


        public ActionResult Export()
        {
            return View();
        }
    }
}