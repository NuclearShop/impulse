using ImpulseApp.Models;
using ImpulseApp.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ImpulseApp
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
            
            ConfigureRoles(app);
            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: "123",
                clientSecret: "123");

            app.UseTwitterAuthentication(
               consumerKey: "123",
               consumerSecret: "123");

            app.UseFacebookAuthentication(
               appId: "123",
               appSecret: "123");

            app.UseGoogleAuthentication();
        }

        private void ConfigureRoles(IAppBuilder app)
        {
  
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            if(!roleManager.RoleExists("Administrators"))
            {
                roleManager.Create(new IdentityRole("Administrators"));
                roleManager.Create(new IdentityRole("Users"));
                roleManager.Create(new IdentityRole("ExtendedUsers"));
                roleManager.Create(new IdentityRole("Moderators"));
            }
            
        }
    }
}