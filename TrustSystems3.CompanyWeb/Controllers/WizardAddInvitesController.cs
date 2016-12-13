using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TrustSystems3.CompanyWeb.Models;
using TrustSystems3.CompanyWeb.Models.Session;
using TrustSystems3.Repository;

namespace TrustSystems3.CompanyWeb.Controllers
{
    [Authorize]
    public class WizardAddInvitesController: BaseController
    {
        private AfsRepository _afsRepository;
        private InvitationRepository _invitationRepository;

        public WizardAddInvitesController()
        {
            _afsRepository = new AfsRepository(DbUnitOfWork);
            _invitationRepository = new InvitationRepository(DbUnitOfWork);
        }

        private InviteInfo GetInviteInfo()
        {
            if (Session["invite_info"] == null) {
                Session["invite_info"] = new InviteInfo();
            }
            return (InviteInfo)Session["invite_info"];
        }

        private void RemoveInviteInfo()
        {
            Session.Remove("invite_info");
        }


        // GET: WizardAddInvites
        public ActionResult Add()
        {
            RemoveInviteInfo();
            return View(new WizardAddInvitesModel());
        }
        [HttpPost]
        public ActionResult Add(WizardAddInvitesModel model, string BtnPrevious, string BtnNext)
        {
            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    InviteInfo inviteInfo = GetInviteInfo();
                    inviteInfo.Data = model.InvitationsData;
                    return View("SendFrom");
                }
            }

            return View(model);
        }


        public ActionResult SendFrom()
        {
            return View(new WizardSendfromInvitesModel());
        }
        [HttpPost]
        public ActionResult SendFrom(WizardSendfromInvitesModel model, string BtnPrevious, string BtnNext)
        {
            InviteInfo inviteInfo = GetInviteInfo();

            if (BtnPrevious != null)
            {
                WizardAddInvitesModel addModel = new WizardAddInvitesModel();
                addModel.InvitationsData = inviteInfo.Data;
                return View("Add", addModel);
            }
            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    inviteInfo.Sender = model.SendFrom;
                    inviteInfo.ReplyTo = model.ReplyTo;

                    var afs = _afsRepository.GetAll().FirstOrDefault(a => a.Companies.Id == GetCurrentCompanyId);
                    return View("PreviewTemplate", afs);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult PreviewTemplate()
        {
            var afs = _afsRepository.GetAll().FirstOrDefault(a => a.Companies.Id == GetCurrentCompanyId);
            return View(afs);
        }
        [HttpPost]
        public ActionResult PreviewTemplate(Afs model, string BtnPrevious, string BtnNext)
        {
            InviteInfo inviteInfo = GetInviteInfo();

            if (BtnPrevious != null)
            {
                WizardSendfromInvitesModel sendModel = new WizardSendfromInvitesModel();
                sendModel.SendFrom = inviteInfo.Sender;
                sendModel.ReplyTo = inviteInfo.ReplyTo;
                return View("SendFrom", sendModel);
            }
            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    String result = String.Empty;
                    StringBuilder sb = new StringBuilder();

                    //Try to create invitations
                    try
                    {
                        string[] lines = inviteInfo.Data.Split(new string[] { Environment.NewLine }, 
                            StringSplitOptions.None);
                        int count = 0;

                        foreach (var line in lines)
                        {
                            var lineItems = line.Split(',');

                            if (lineItems.Length == 3)
                            {
                                //If email already in invites, skip it
                                if (!_invitationRepository.GetAll()
                                    .Any(i => i.Email == lineItems[0] && i.CompaniesId == GetCurrentCompanyId))
                                {
                                    _invitationRepository.Insert(new Invitation
                                    {
                                        CompaniesId = GetCurrentCompanyId,
                                        CreatedAt = DateTime.Now,
                                        DeliveryDate = DateTime.Now.AddDays(model.TemplateDelay),
                                        Email = lineItems[0],
                                        SenderName = lineItems[1],
                                        ReferenceId = lineItems[2],
                                        FromName = inviteInfo.Sender,
                                        FromEmail = inviteInfo.ReplyTo,
                                        Status = InivationStatusType.Pending
                                    });

                                    sb.AppendFormat("{0} ", lineItems[0]);
                                    count ++;
                                }
                            }
                        }

                        result = (count > 0)
                            ? String.Format("Успешно созднаны приглашения для следующих email адресов ({0}): {1}", count, sb)
                            : "Ни один из email не обработан. Такие email или уже есть в приглашениях.";
                    }
                    catch (Exception ex)
                    {
                        result = string.Format("Во время обработки приглашений произошла ошбика: {0}", ex.Message);
                    }

                    RemoveInviteInfo();
                    return RedirectToAction("Done", new { result = result });
                }
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Done(string result)
        {
            ViewBag.Result = result;
            return View();
        }
    }
}