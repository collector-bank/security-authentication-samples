using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;


[assembly: OwinStartup(typeof(CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientOwin.Startup))]
namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientOwin
{

    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://idp-uat.collectorbank.se/",
                ClientId = "MZxDS_9hY64cva_-V9eV",
                RedirectUri = "https://localhost:45100/signin",
                ResponseType = "id_token",    // Need to be specified in order for OAuth OpenID Connect implicit flow to be used
                Notifications = new OpenIdConnectAuthenticationNotifications    // This is needed if you want to controll the authentication method and ui local that is used
                {
                    RedirectToIdentityProvider = ctx =>
                    {
                        // The desired authentication method. Allowed values are: sbid, nbid and tupas.
                        ctx.ProtocolMessage.LoginHint = "tupas";
                        // The desired UI locales.
                        // Value can be one or more of the following locales (sv, nb, fi, en) seperated by space where the first UI locales in the list that the authenication method supports will be used.
                        ctx.ProtocolMessage.UiLocales = "sv nb";
                        return Task.CompletedTask;
                    }
                }
            });
        }
    }
}