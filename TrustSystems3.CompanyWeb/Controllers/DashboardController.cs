using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using TrustSystems3.BL.Email;
using TrustSystems3.CompanyWeb.Helpers;
using TrustSystems3.CompanyWeb.Models;
using TrustSystems3.CompanyWeb.Models.ModelEntity;
using TrustSystems3.Repository;
using System.Collections.Generic;

namespace TrustSystems3.CompanyWeb.Controllers
{
    [Authorize]
    public class DashboardController: BaseController
    {
        private UserRepository _userRepository;
        private ReviewRepository _reviewRepository;
        private ApplicationUserManager _userManager;

        private AfsRepository _afsRepository;
        private TemplateRepository _templateRepository;
        private Companies _currentCompany;

        private Companies CurrentCompany
        {
            get
            {
                if (_currentCompany == null)
                {
                    _currentCompany = DbCompanyRepository.GetAll().FirstOrDefault(c => c.Id == GetCurrentCompanyId);
                }
                return _currentCompany;
            }
        }

        public DashboardController()
        {
            _userRepository = new UserRepository(DbUnitOfWork);
            _reviewRepository = new ReviewRepository(DbUnitOfWork);

            _afsRepository = new AfsRepository(DbUnitOfWork);
            _templateRepository = new TemplateRepository(DbUnitOfWork);
        }

        #region Properties

        public bool IsCompanyOwner
        {
            get
            {
                return Session[ConstantKeys.CURRENT_USER_STATE] != null &&
                       (UserStateType) Session[ConstantKeys.CURRENT_USER_STATE] == UserStateType.Admin;
            }
        }

        public ApplicationUserManager UserManager {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        #endregion

        // GET: Dashboard
        public ActionResult Index()
        {
            return View(CurrentCompany);
        }

        public ActionResult Modules()
        {
            return View();
        }

        public ActionResult Reviews()
        {
            var reviews = _reviewRepository.GetReviewsByCompanyId(GetCurrentCompanyId);
            List<UserReview> list = new List<UserReview>();
            
            foreach (var review in reviews) {
                var userReview = new UserReview {
                    Review = review,
                    User = _userRepository.Single(review.UserId)
                };
                list.Add(userReview);
            }
            
            return View(list);
        }

        public ActionResult Statistics()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        #region Email template

        [HttpGet]
        public ActionResult EmailTemplate()
        {
            var afs = _afsRepository.GetAll().FirstOrDefault(a => a.Companies.Id == GetCurrentCompanyId);
            if (afs != null)
            {
                //update template if afs setting is empty
                if (afs.TemplateSubject == null && afs.TemplateBody == null)
                {
                    var template = _templateRepository.SingleOrDefault(afs.Id);
                    if (template != null)
                    {
                        afs.TemplateSubject = template.Subject;
                        afs.TemplateBody = template.Body;

                        _afsRepository.Update(afs);
                    }
                }

                return View(new EmailTemplateViewModel
                {
                    Delay = afs.TemplateDelay,
                    Subject = afs.TemplateSubject,
                    Body = afs.TemplateBody
                });
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public ActionResult EmailTemplate(EmailTemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var afs = _afsRepository.GetAll().FirstOrDefault(a => a.Companies.Id == GetCurrentCompanyId);
            if (afs != null)
            {
                afs.TemplateDelay = model.Delay;
                afs.TemplateSubject = model.Subject;
                afs.TemplateBody = model.Body;

                _afsRepository.Update(afs);
            }

            return RedirectToAction("Modules", "Dashboard");
        }

        [HttpPost]
        public JsonResult UpdateTemplate(int languageId)
        {
            var template = _templateRepository.GetAll().FirstOrDefault(t => t.Language == languageId);
            if (template != null) {
                return Json(new {
                    subject = template.Subject,
                    body = template.Body
                });
            }

            return null;
        }

        #endregion

        [HttpGet]
        public ActionResult UserManagement()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UserManagement(string email)
        {

            if (_userRepository.GetAll().Any(u => u.Email == email))
            {
                var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);

                // 1. is user has and account in system (exists)

                // 1.1 if user already in company - warn message
                if (user == null || !DbCompanyUserRepository.IsUserBelongAnyCompany(user.Id))
                {
                    return RedirectToAction("UserManagement")
                        .Warning(string.Format(
                                "Пользователь {0} не может быть добавлен, т.к. уже управляет компанией (этой или какой-либо другой)", email));
                }
                else
                {
                    // add user
                    DbCompanyUserRepository.Insert(new CompanyUsers
                    {
                        UserId = user.Id,
                        CompanyId = GetCurrentCompanyId,
                        UserState = UserStateType.Approved
                    });

                    // send notify message via e-mail
                    using (var emailClient = new EmailClient("george2code@gmail.com", "dominicborn@gmail.com", "Test message"))
                    {
                        emailClient.SendInviteNotifyEmail(user.UserName, "pasdd12345821", "WelcomeEmail.txt");
                    }

                    return RedirectToAction("UserManagement")
                        .Success(string.Format("Пользователь {0} добавлен в компанию!", email));
                }

            }
            else
            {
                //TODO: test!!!!!!!!!!!!!!!!!!!!!!
                // 2. if user not exists
    
                // 2.1 create user
                var user = new ApplicationUser
                {
                    UserName = email, 
                    Email = email
                };

                var result = await UserManager.CreateAsync(user, "Test123!");

                if (result.Succeeded)
                {
                    // 2.2 send email with code
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                     var callbackUrl = Url.Action("ConfirmEmail", "Dashboard", 
                         new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                     await UserManager.SendEmailAsync(user.Id, "Confirm your account", 
                         "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    // 2.3 add this user to companyUsers with Pending state    
                }
            }

            

            return RedirectToAction("UserManagement").Success("Message shown to user after redirect");
//            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}