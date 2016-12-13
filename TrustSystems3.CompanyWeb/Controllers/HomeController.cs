using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TrustSystems3.CompanyWeb.Helpers;
using TrustSystems3.CompanyWeb.Models;
using TrustSystems3.CompanyWeb.Models.Account;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.CompanyWeb.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _uow = null;
        private CompanyRepository _companyRepository = null;
        private CompanyUserRepository _companyUserRepository = null;
        private ContactRepository _contactRepository = null;
        private AfsRepository _afsRepository;
        private TemplateRepository _templateRepository;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
            _uow = new UnitOfWork();

            _companyRepository = new CompanyRepository(_uow);
            _companyUserRepository = new CompanyUserRepository(_uow);
            _contactRepository = new ContactRepository(_uow);
            _afsRepository = new AfsRepository(_uow);
            _templateRepository = new TemplateRepository(_uow);
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #region Properties

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Whyus()
        {
            return View();
        }

        #region Login and Signup

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, 
                shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:

                    // save companyId to session
                    var companyUser = _companyUserRepository.GetAll().FirstOrDefault(c => c.UserId == User.Identity.GetUserId());
                    if (companyUser != null)
                    {
                        Session[ConstantKeys.CURRENT_COMPANY_ID] = companyUser.CompanyId;
                        Session[ConstantKeys.CURRENT_USER_STATE] = companyUser.UserState;
                    }

                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
//                case SignInStatus.RequiresVerification:
//                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Home/Signup
        [AllowAnonymous]
        public ActionResult Signup()
        {
            return View();
        }

        //
        // POST: /Home/Signup
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(SignupViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                // Company name
                var company = _companyRepository.GetAll().Where(c => c.Name == model.CompanyName);
                if (company.Any())
                {
                    ModelState.AddModelError(string.Empty, "Company name already taken!");
                    return RedirectToAction("Index", "Home");
                }

                // Create user if company passed
                var user = new ApplicationUser
                {
                    UserName = model.Email, 
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    _contactRepository.Insert(new Contacts
                    {
                        UserId = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Country = model.Country.HasValue ? (int?)model.Country.Value : null
                    });


                    var afsSettings = new Afs {
                        SenderName = string.Format("{0} {1}", model.FirstName, model.LastName),
                        InviteFrequency = InviteFrequencyType.Always,
                        TemplateDelay = 7,
                        Template = _templateRepository.GetAll().First()
                    };

                    // register company 
                    var newCompany = new Companies {
                        Name = model.CompanyName,
                        Email = model.Email,
                        Website = model.Website,
                        PhoneNumber = model.PhoneNumber,
                        Country = model.Country.HasValue ? (int?)model.Country.Value : null,
                        Afs = afsSettings
                    };
                    _companyRepository.Insert(newCompany);

                    
                    // add UserCompany
                    _companyUserRepository.Insert(new CompanyUsers
                    {
                       UserId = user.Id,
                       CompanyId = newCompany.Id,
                       UserState = UserStateType.Admin,
                       IsOwner = 1
                    });


                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Dashboard");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Methods

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            return RedirectToAction("Index", "Dashboard");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #endregion
    }
}