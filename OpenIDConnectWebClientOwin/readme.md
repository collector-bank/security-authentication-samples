
# OpenIDConnectWebClientOwin
OpenID Connect Implict flow sample using  ASP.NET using .NET Framework with OWIN

 1. [Nuget Packages](#nuget-packages)
 1. [Code needed](#code-needed)
 1. [Configure SSL/TLS Port on IIS Express manually](#configure-ssltls-port-on-iis-express-manually)

---
## Nuget Packages
You will need to include the following Nuget packages:

 * Microsoft.Owin.Security.OpenIdConnect
 * Microsoft.Owin.Security.Cookies
 * Microsoft.Owin.Host.SystemWeb (needed otherwise OwinStartup will not call Configuration method for the class specified)

---
## Code needed
Create Startup.cs under App_Start folder.

In the class create the following method **public void Configuration(IAppBuilder app)**.
In the namespace above the class add **[assembly: OwinStartup(typeof(Startup))]**

In the **public void Configuration(IAppBuilder app)** you need to configure OpenID Connect by adding the following code
```c
    app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
    app.UseCookieAuthentication(new CookieAuthenticationOptions());
    app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
    {
        Authority = "https://web-idpserver-auth-test.azurewebsites.net/",
        ClientId = "btwzQWmTSKeKfubsTzdYvw",
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
```

If LoginHint is not specified then the default authentication method for the specified ClientId will be used.  
If UiLocales is not specified then the default local for the authentication method will be used.

---
## Configure SSL/TLS Port on IIS Express manually
The port used for the example are 45100.  
If SSL/TLS is not setup on that port you can configure it manually.

1. Open up an elevated Command Prompt (i.e. run Command Prompt as administrator)
1. Type command> netsh http show sslcert
1. Copy the certhash and appid of an existing entry
1. Copy/Type command> netsh http add sslcert ipport=0.0.0.0:45100 certhash=<certhash> appid=<appid>
   where you replace certhash and appid with the ones from an existing entry
   Example> netsh http add sslcert ipport=0.0.0.0:45100 certhash=241c2e7bcc16c2d772ac9a0e69ccfb36d45b95b9 appid={21d22dcd-d05b-4349-9bf9-9cdd44b2b74a}
