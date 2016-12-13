using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using TrustSystems3.ClientWeb.Models;
using TrustSystems3.ClientWeb.Utils.VkAuth;
using TrustSystems3.ClientWeb.Utils.VkAuth.Provider;

namespace TrustSystems3.ClientWeb
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            
            var facebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings["fb_auth_id"],
                AppSecret = ConfigurationManager.AppSettings["fb_auth_secret"],
                AuthenticationType = "Facebook",
                SignInAsAuthenticationType = "ExternalCookie",
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = async context =>
                    {
                        context.Identity.AddClaim(new Claim("FacebookAccessToken", context.AccessToken));
                        foreach (var claim in context.User)
                        {
                            var claimType = string.Format("urn:facebook:{0}", claim.Key);
                            string claimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, claimValue))
                                context.Identity.AddClaim(new Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));

                        }

                    }
                }
            };
            app.UseFacebookAuthentication(facebookAuthenticationOptions);

            // ------------------------------------------------------------------------------------------------

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["gplus_auth_id"],
                ClientSecret = ConfigurationManager.AppSettings["gplus_auth_secret"],
                CallbackPath = new PathString("/Account/ExternalGoogleLoginCallback"),
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                    {
                        foreach (var claim in context.User)
                        {
                            string claimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claim.Key, claimValue))
                                context.Identity.AddClaim(new Claim(claim.Key, claimValue));
                        }
                    }
                }
            };
            googleOAuth2AuthenticationOptions.Scope.Add("email");
            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);

            // ------------------------------------------------------------------------------------------------

            var vkOptions = new VkAuthenticationOptions
            {
                AppId = ConfigurationManager.AppSettings["vk_auth_id"],
                AppSecret = ConfigurationManager.AppSettings["vk_auth_secret"],
                Scope = "photos,offline,email,photo_200",
                Provider = new VkAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {

                        context.Identity.AddClaim(new Claim("urn:vkontakte:access_token", context.AccessToken, ClaimValueTypes.String, "Vkontakte"));
                        context.Identity.AddClaim(new Claim("urn:vkontakte:email", context.Email, ClaimValueTypes.Email, "Vkontakte"));

                        context.Identity.AddClaim(new Claim("urn:vkontakte:first_name", context.Name, ClaimValueTypes.String, "Vkontakte"));
                        context.Identity.AddClaim(new Claim("urn:vkontakte:last_name", context.LastName, ClaimValueTypes.String, "Vkontakte"));

                        context.Identity.AddClaim(new Claim("urn:vkontakte:photo_big", context.Avatar, ClaimValueTypes.String, "Vkontakte"));
                        context.Identity.AddClaim(new Claim("urn:vkontakte:sex", context.Sex, ClaimValueTypes.String, "Vkontakte"));
                        context.Identity.AddClaim(new Claim("urn:vkontakte:bdate", context.Bdate, ClaimValueTypes.String, "Vkontakte"));
                        
                        return Task.FromResult(0);
                    }
                }
            };
            app.UseVkontakteAuthentication(vkOptions);
        }
    }
}