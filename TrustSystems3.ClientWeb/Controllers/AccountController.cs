using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using TrustSystems3.ClientWeb.Models;
using TrustSystems3.ClientWeb.Utils;
using TrustSystems3.Repository;
using TrustSystems3.Repository.Infrastructure;
using TrustSystems3.Repository.Infrastructure.Contract;

namespace TrustSystems3.ClientWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private IUnitOfWork _uow = null;
        private UserRepository _userRepository = null;

        public AccountController()
        {
            _uow = new UnitOfWork();
            _userRepository = new UserRepository(_uow);
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;

            _uow = new UnitOfWork();
            _userRepository = new UserRepository(_uow);
        }

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
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }



            StringBuilder sb = new StringBuilder();
            foreach (var claim in loginInfo.ExternalIdentity.Claims)
            {
                sb.AppendFormat("{0} | ", claim.Value);
            }

            
            if (HttpContext.Request.Cookies.AllKeys.Contains("TESTSYSTEM"))
            {
                HttpCookie cookie = HttpContext.Request.Cookies["TESTSYSTEM"];
                cookie.Value = sb.ToString();
                HttpContext.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie cookie = new HttpCookie("TESTSYSTEM");
                cookie.Value = sb.ToString();
                HttpContext.Response.Cookies.Add(cookie);
            }


            //TODO: проверить регистрацию. Если заходит через соц сеть, и нет аккаунта, создает без пароля. Регистрация через почту на этот
            //TODO: почтовый ящик запрещена уже.
            var aspNetUser = _userRepository.GetAll().FirstOrDefault(u => u.Email == loginInfo.Email);
            if (aspNetUser == null)
            {
                // Create user with no password
                var appUser = new ApplicationUser { UserName = loginInfo.Email, Email = loginInfo.Email };
                var result3 = await UserManager.CreateAsync(appUser);
                if (result3.Succeeded)
                {
                    await UserManager.AddLoginAsync(appUser.Id, loginInfo.Login);
                }
            }

            
            var user = UpdateSocialInfoUser(loginInfo, aspNetUser);
            var result = (user != null) ? SignInStatus.Success : SignInStatus.Failure;

            // Sign in the user with this external login provider if the user already has a login
//            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:

                     
                    var applicationUser = UserManager.Users.FirstOrDefault(u => u.Email == user.Email);
                    if (applicationUser != null)
                    {
                        await SignInManager.SignInAsync(applicationUser, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }

//                    UpdateSocialInfoUser(loginInfo);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                    return RedirectToAction("Register");
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }


        [AllowAnonymous]
        public async Task<ActionResult> ExternalGoogleLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }


            //TODO: проверить регистрацию. Если заходит через соц сеть, и нет аккаунта, создает без пароля. Регистрация через почту на этот
            //TODO: почтовый ящик запрещена уже.
            var aspNetUser = _userRepository.GetAll().FirstOrDefault(u => u.Email == loginInfo.Email);
            if (aspNetUser == null)
            {
                // Create user with no password
                var appUser = new ApplicationUser { UserName = loginInfo.Email, Email = loginInfo.Email };
                var result3 = await UserManager.CreateAsync(appUser);
                if (result3.Succeeded)
                {
                    await UserManager.AddLoginAsync(appUser.Id, loginInfo.Login);
                }
            }


            var user = UpdateSocialInfoUser(loginInfo, aspNetUser);

            var applicationUser = UserManager.Users.FirstOrDefault(u => u.Email == user.Email);
            if (applicationUser != null)
            {
                await SignInManager.SignInAsync(applicationUser, isPersistent: false, rememberBrowser: false);
            }

            
            return RedirectToAction("Index", "Home");
        }



        private AspNetUsers UpdateSocialInfoUser(ExternalLoginInfo info, AspNetUsers user)
        {
            if (user == null)
            {
                user = _userRepository.GetAll().FirstOrDefault(u => u.Email == info.Email);
            }


            if (user != null)
            {
                var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
//                user.UserName = externalIdentity.Result.Name;

                switch (info.Login.LoginProvider)
                {
                    case "Google":

                        using (SocialInfoParser socialInfoParser = new SocialInfoParser(externalIdentity.Result.Claims))
                        {
                            // FirstName & LastName
                            var g_firstName = socialInfoParser.GetJsonItem("name", "givenName");
                            if (g_firstName != null)
                                user.FirstName = g_firstName;

                            var g_lastName = socialInfoParser.GetJsonItem("name", "familyName");
                            if (g_lastName != null)
                                user.LastName = g_lastName;

                            // Birthdate
                            // -

                            // About
                            var g_bio = socialInfoParser.GetString("occupation");
                            if (g_bio != null)
                                user.About = g_bio;
                            // Avatar
                            var g_avatar = socialInfoParser.GetJsonItem("image", "url");
                            if (g_avatar != null)
                                user.Avatar = g_avatar.Replace("sz=50", "sz=250");
                            // Gender
                            var g_gender = socialInfoParser.GetString("gender");
                            if (g_gender != null)
                                user.Gender = g_gender;
                            // ExtraAddress
                            var g_address = socialInfoParser.GetJsonArrayItem("placesLived", "value");
                            if (g_address != null)
                                user.ExternalAddress = g_address;
                        }

                        break;


                    case "Facebook":

                        // First Name & Last Name
                        var firstName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:first_name");
                        if (firstName != null)
                            user.FirstName = firstName.Value;

                        var lastName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:last_name");
                        if (lastName != null)
                            user.LastName = lastName.Value;

                        // Birthdate
                        var birthDate = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:birthday");
                        if (birthDate != null)
                        {
                            DateTime ukDateFormat;
                            DateTime.TryParseExact(birthDate.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ukDateFormat);
                            user.BirthDate = ukDateFormat;
                        }

                        // About
                        var bio = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:bio");
                        if (bio != null)
                            user.About = bio.Value;

                        // Avatar
                        user.Avatar = string.Format("https://graph.facebook.com/{0}/picture?width=250&height=250", 
                            externalIdentity.Result.GetUserId());

                        // Gender
                        var gender = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:gender");
                        if (gender != null)
                            user.Gender = gender.Value;

                        // ExtraAddress
                        var address = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:location");
                        if (address != null)
                        {
                            JObject obj = JObject.Parse(address.Value);
                            if (obj != null && obj.HasValues && obj["name"] != null)
                            {
                                user.ExternalAddress = obj["name"].ToString();
                            }
                        }

                        break;


                    case "Vkontakte":
                        // First Name & Last Name
                        var vFirstName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:vkontakte:first_name");
                        if (vFirstName != null)
                            user.FirstName = vFirstName.Value;

                        var vLastName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:vkontakte:last_name");
                        if (vLastName != null)
                            user.LastName = vLastName.Value;

                        // Birthdate
//                        var vBirthDate = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:vkontakte:bdate");
//                        if (vBirthDate != null)
//                        {
//                            DateTime ukDateFormat;
//                            DateTime.TryParseExact(vBirthDate.Value, "d/M", CultureInfo.InvariantCulture, DateTimeStyles.None, out ukDateFormat);
//                            user.BirthDate = ukDateFormat;
//                        }

                        // Avatar
                        var vAvatar = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:vkontakte:photo_big");
                        if (vAvatar != null)
                            user.Avatar = vAvatar.Value;

                        // Gender
                        var vGender = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:vkontakte:sex");
                        if (vGender != null)
                            user.Gender = Convert.ToInt32(vGender.Value) == 1 ? "female" : "male";
                        break;


                    default:
                        break;
                }


                _userRepository.Update(user);
            }

            return user;
        }





        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
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

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

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
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}