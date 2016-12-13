using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using TrustSystems3.BL.Email;
using TrustSystems3.BL.Utils;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.CompanyWeb.Controllers
{
    public class JobsController : Controller
    {
        private IUnitOfWork _uow;
        private InvitationRepository _invitationRepository;
        private AfsRepository _afsRepository;
        private CompanyRepository _companyRepository;
        private LoggerRepository _loggerRepository;


        public JobsController()
        {
            _uow = new UnitOfWork();
            _invitationRepository = new InvitationRepository(_uow);
            _afsRepository = new AfsRepository(_uow);
            _companyRepository = new CompanyRepository(_uow);
            _loggerRepository = new LoggerRepository(_uow);
        }



        //
        // GET: /Jobs/
        // every 12 hours
        public ActionResult SendInvitations()
        {
            try
            {
                var sentList = _invitationRepository.GetAll().Where(
                    m => m.Status == InivationStatusType.Pending
                         && m.DeliveryDate.HasValue
                         && m.DeliveryDate.Value.Date == DateTime.Today);

                var invitations = sentList as Invitation[] ?? sentList.ToArray();
                if (invitations.Any())
                {
                    List<String> completedEmails = new List<string>();
                    List<String> errorEmails = new List<string>();

                    foreach (var item in invitations)
                    {
                        var company = _companyRepository.Single(item.CompaniesId);
                        var afs = _afsRepository.GetAll().FirstOrDefault(a => a.Companies.Id == item.CompaniesId);

                        if (company != null && afs != null)
                        {
                            var link = string.Format("{0}/review/{1}",
                                ConfigurationManager.AppSettings["host"],
                                company.Slug);

                            var to = item.Email;
                            var toName = item.SenderName ?? "customer";
                            var from = item.FromEmail ?? ConfigurationManager.AppSettings["email"];
                            //                            var fromName = item.FromName ?? "TrustSystems";

                            var orderId = item.ReferenceId;
                            var domainName = company.Website;

                            var emailSubject = TemplateFilter.Proceed(afs.TemplateSubject, link, toName, domainName,
                                orderId);
                            var emailBody = TemplateFilter.Proceed(afs.TemplateBody, link, toName, domainName, orderId);

                            using (var emailClient = new EmailClient(from, to, emailSubject))
                            {
                                item.Status = emailClient.SendInvite(emailBody, @from, "TrustSystem2015048")
                                    ? InivationStatusType.Sent
                                    : InivationStatusType.Error;

                                _invitationRepository.Update(item);
                            }

                            completedEmails.Add(item.Email);
                        }
                        else
                        {
                            errorEmails.Add(item.Email);
                        }
                    }

                    // add log
                    StringBuilder sb = new StringBuilder();
                    sb.Append("New invitations was found. ");
                    if (completedEmails.Any())
                    {
                        sb.AppendFormat("Completed: {0}.", string.Join(",", completedEmails.ToArray()));
                    }
                    if (errorEmails.Any())
                    {
                        sb.AppendFormat("With errors: {0}.", string.Join(",", errorEmails.ToArray()));
                    }
                    _loggerRepository.AddLog(sb.ToString(), LogType.SchedulerEmailJob);
                    // end log
                }
                else
                {
                    _loggerRepository.AddLog("There's no invitations found to send emails", LogType.SchedulerInviteJob);
                }
            }
            catch (Exception ex)
            {
                _loggerRepository.AddLog(ex.Message, LogType.SchedulerInviteJob);
            }
            finally
            {
                _uow.Dispose();
            }

            return View();
        }


        // every 11 hours
        public ActionResult CheckEmailsForInvites()
        {
            try
            {
                using (Imap imap = new Imap())
                {
                    imap.ConnectSSL("imap.gmail.com");    // or ConnectSSL for SSL
                    imap.UseBestLogin("trustsystems.company@gmail.com", "TrustSystem2015048");

                    imap.SelectInbox();
                    List<long> uids = imap.Search(Flag.Unseen);

                    if (uids.Count > 0)
                    {
                        List<String> completedEmails = new List<string>();
                        List<String> errorEmails = new List<string>();

                        foreach (long uid in uids)
                        {
                            var eml = imap.GetMessageByUID(uid);
                            IMail email = new MailBuilder().CreateFromEml(eml);

                            var list = StringUtils.ExtractEmails(email.Text);

                            if (list.Count > 0)
                            {
                                string clientEmailAddress = list[0];

                                //find company by email
                                var company = _companyRepository.GetAll().FirstOrDefault(c => c.Email == email.Sender.Address);

                                if (company != null)
                                {
                                    var afs = _afsRepository.GetAll().FirstOrDefault(a => a.Companies.Id == company.Id);

                                    //avoid to add invitation on existing email
                                    if (afs != null && _invitationRepository.GetAll().All(x => x.Email != clientEmailAddress))
                                    {
                                        _invitationRepository.Insert(new Invitation
                                        {
                                            CompaniesId = company.Id,
                                            Email = clientEmailAddress,
                                            Status = InivationStatusType.Pending,
                                            CreatedAt = DateTime.Now,
                                            ReferenceId = Guid.NewGuid().ToString(),
                                            DeliveryDate = DateTime.Now.AddDays(afs.TemplateDelay),
                                            SenderName = null,
                                            FromEmail = null,
                                            FromName = null
                                        });

                                        completedEmails.Add(clientEmailAddress);
                                    }
                                    else
                                    {
                                        errorEmails.Add(clientEmailAddress);
                                    }
                                }
                                else
                                {
                                    //TODO: set no company found to log
                                    errorEmails.Add(email.Sender.Address);
                                }


                                // add log
                                StringBuilder sb = new StringBuilder();
                                sb.Append("New emails was found. ");
                                if (completedEmails.Any())
                                {
                                    sb.AppendFormat("Completed: {0}.", string.Join(",", completedEmails.ToArray()));
                                }
                                if (errorEmails.Any())
                                {
                                    sb.AppendFormat("With errors: {0}.", string.Join(",", errorEmails.ToArray()));
                                }
                                _loggerRepository.AddLog(sb.ToString(), LogType.SchedulerEmailJob);
                                // end log
                            }
                            else
                            {
                                _loggerRepository.AddLog("There's no emails was found", LogType.SchedulerEmailJob);
                            }
                            

                            //mark as read
                            imap.MarkMessageSeenByUID(uid);
                        }
                    }
                    else
                    {
                        _loggerRepository.AddLog("There's no emails was found", LogType.SchedulerEmailJob);
                    }

                    
                    imap.Close();
                }

            }
            catch (Exception ex)
            {
                _loggerRepository.AddLog(ex.Message, LogType.SchedulerEmailJob);
            }
            finally
            {
                _uow.Dispose();
            }

            return View();
        }







//        public ActionResult CheckEmailsForInvites()
//        {
//            try
//            {
//                // Gmail IMAP4 server is "imap.gmail.com"
//                MailServer oServer = new MailServer("imap.gmail.com",
//                            "trustsystems.company@gmail.com", "TrustSystem2015048", ServerProtocol.Imap4);
//                MailClient oClient = new MailClient("1A068A1BF5CA1C8FF115709E96B4E9ED");
//
//
//                // Set SSL connection,
//                oServer.SSLConnection = true;
//
//                // Set 993 IMAP4 port
//                oServer.Port = 993;
//
//                oClient.GetMailInfosParam.GetMailInfosOptions = GetMailInfosOptionType.NewOnly;
//                oClient.Connect(oServer);
//                MailInfo[] infos = oClient.GetMailInfos();
//
//                if (infos.Length > 0)
//                {
//                    List<String> completedEmails = new List<string>();
//                    List<String> errorEmails = new List<string>();
//
//
//                    for (int i = 0; i < infos.Length; i++)
//                    {
//                        MailInfo info = infos[i];
//                        Mail oMail = oClient.GetMail(info);
//
//                        var list = StringUtils.ExtractEmails(oMail.TextBody);
//
//                        if (list.Count > 0)
//                        {
//                            var email = list[0];
//
//                            //find company by email
//                            var company = _companyRepository.GetAll().FirstOrDefault(c => c.Email == email);
//
//                            if (company != null)
//                            {
//                                var afs = _afsRepository.GetAll().FirstOrDefault(a => a.Companies.Id == company.Id);
//
//                                //avoid to add invitation on existing email
//                                if (afs != null && _invitationRepository.GetAll().All(x => x.Email != email))
//                                {
//                                    _invitationRepository.Insert(new Invitation
//                                    {
//                                        CompaniesId = company.Id,
//                                        Email = email,
//                                        Status = InivationStatusType.Pending,
//                                        CreatedAt = DateTime.Now,
//                                        ReferenceId = Guid.NewGuid().ToString(),
//                                        DeliveryDate = DateTime.Now.AddDays(afs.TemplateDelay),
//                                        SenderName = null,
//                                        FromEmail = null,
//                                        FromName = null
//                                    });
//
//                                    completedEmails.Add(email);
//                                }
//                                else
//                                {
//                                    errorEmails.Add(email);
//                                }
//                            }
//                            else
//                            {
//                                //TODO: set no company found to log
//                                errorEmails.Add(email);
//                            }
//
//
//                            // add log
//                            StringBuilder sb = new StringBuilder();
//                            sb.Append("New emails was found. ");
//                            if (completedEmails.Any())
//                            {
//                                sb.AppendFormat("Completed: {0}.", string.Join(",", completedEmails.ToArray()));
//                            }
//                            if (errorEmails.Any())
//                            {
//                                sb.AppendFormat("With errors: {0}.", string.Join(",", errorEmails.ToArray()));
//                            }
//                            _loggerRepository.AddLog(sb.ToString(), LogType.SchedulerEmailJob);
//                            // end log
//                        }
//                        else
//                        {
//                            _loggerRepository.AddLog("There's no emails was found", LogType.SchedulerEmailJob);
//                        }
//
//
//                        oClient.MarkAsRead(info, true);
//
//                        // Mark email as deleted in GMail account.
//                        //oClient.Delete(info);
//                    }
//
//
//                    // Quit and pure emails marked as deleted from Gmail IMAP4 server.
//                    oClient.Quit();
//                }
//                else
//                {
//                    _loggerRepository.AddLog("There's no emails was found", LogType.SchedulerEmailJob);
//                }
//            }
//            catch (Exception ex)
//            {
//                _loggerRepository.AddLog(ex.Message, LogType.SchedulerEmailJob);
//            }
//            finally
//            {
//                _uow.Dispose();
//            }
//
//            return View();
//        }


	}
}