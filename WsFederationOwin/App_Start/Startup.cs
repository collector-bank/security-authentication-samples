using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.WsFederation;
using Owin;


[assembly: OwinStartup(typeof(CollectorBank.Security.Authentication.Samples.WsFederationOwin.Startup))]
namespace CollectorBank.Security.Authentication.Samples.WsFederationOwin
{

    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseWsFederationAuthentication(new WsFederationAuthenticationOptions
            {
                Wtrealm = "https://localhost/wsfed",
                Wreply = "https://localhost:45200/signin-wsfed",
                MetadataAddress = "https://idp-uat.collectorbank.se/2007-06/FederationMetadata.xml",
                Notifications = new WsFederationAuthenticationNotifications    // Need to be specified in order for OAuth OpenID Connect implicit flow to be used
                {
                    RedirectToIdentityProvider = ctx =>
                    {
                        // The desired authentication method is passed in the custom coauth parameter. Allowed values are: sbid, nbid and tupas.
                        ctx.ProtocolMessage.SetParameter("coauth", "tupas");
                        // The desired UI locales are passed in the custom colocales parameter.
                        // Value can be one or more of the following locales (sv, nb, fi, en) seperated by space where the first UI locales in the list that the authenication method supports will be used.
                        ctx.ProtocolMessage.SetParameter("colocales", "fi, sv, en");
                        return Task.CompletedTask;
                    }
                }
            });
        }
    }
}