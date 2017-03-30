
# OpenIDConnectWebClientCore
OpenID Connect Implict flow sample using ASP.NET Core using .NETCore 1.1.0 framework.

 1. [Nuget Packages](#nuget-packages)
 1. [Code needed](#code-needed)
 1. [Configure SSL/TLS Port on IIS Express manually](#configure-ssltls-port-on-iis-express-manually)

---
## Nuget Packages
You will need to include the following Nuget packages:

 * Microsoft.AspNetCore.Authentication.OpenIdConnect
 * Microsoft.AspNetCore.Authentication.Cookies

---
## Code needed
You need to activate authentication in **Startup.cs** method **public void ConfigureServices(IServiceCollection services)** by adding
```cs
    services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
```

The you need to configure OpenID Connect. This is done in **Startup.cs** method **public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)**
```c
    app.UseCookieAuthentication(new CookieAuthenticationOptions
    {
        AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme
    });

    app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
    {
        SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme,
        Authority = "https://idp-uat.collectorbank.se/",
        ClientId = "MZxDS_9hY64cva_-V9eV",
        Events = new OpenIdConnectEvents    // This is needed if you want to controll the authentication method and ui local that is used
        {
            OnRedirectToIdentityProvider = ctx =>
            {
                // LoginHint contain the desired authentication method of nbid along with authentication hint of the end user.
                // Security warning information disclosure the national identifier will be sent in the front channel
                // reveling information about the expected end user that should authenticated themselves.
                ctx.ProtocolMessage.LoginHint = "nbid_21048349827";
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
The port used for the example are 45000.  
If SSL/TLS is not setup on that port you can configure it manually.

1. Open up an elevated Command Prompt (i.e. run Command Prompt as administrator)
1. Type command> netsh http show sslcert
1. Copy the certhash and appid of an existing entry
1. Copy/Type command> netsh http add sslcert ipport=0.0.0.0:45000 certhash=<certhash> appid=<appid>
   where you replace certhash and appid with the ones from an existing entry
   Example> netsh http add sslcert ipport=0.0.0.0:45000 certhash=241c2e7bcc16c2d772ac9a0e69ccfb36d45b95b9 appid={21d22dcd-d05b-4349-9bf9-9cdd44b2b74a}
---