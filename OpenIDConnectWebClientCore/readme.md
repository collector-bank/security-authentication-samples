
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
You need to configure authentication in **Startup.cs** method **public void ConfigureServices(IServiceCollection services)** by adding
```cs
    services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddOpenIdConnect(options =>
        {
            options.ClientId = "MZxDS_9hY64cva_-V9eV";
            options.ClientSecret = "secret";
            options.Authority = "https://idp-uat.collectorbank.se/";
            options.ResponseType = OpenIdConnectResponseType.Code;
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Events.OnRedirectToIdentityProvider = ctx => 
              // This is needed if you want to controll the authentication method and ui local that is used, per login request
            {
                ctx.ProtocolMessage.AcrValues = "urn:collectorbank:ac:method:nbid"; // Set desired login method
                ctx.ProtocolMessage.UiLocales = "sv nb"; // And desired language
                return Task.CompletedTask;
            };
        });
```

Then you need to activate Authentication aswell. This is done in **Startup.cs** method **public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)**
```c
    app.UseAuthentication();
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