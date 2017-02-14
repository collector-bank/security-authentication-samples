using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                Authority = "https://web-idpserver-auth-test.azurewebsites.net/",
                ClientId = "btwzQWmTSKeKfubsTzdYvw",
                Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProvider = ctx =>
                    {
                        // The desired authentication method. Allowed values are: sbid, nbid and tupas.
                        ctx.ProtocolMessage.LoginHint = "nbid";
                        // The desired UI locales.
                        // Value can be one or more of the following locales (sv, nb, fi, en) seperated by space where the first UI locales in the list that the authenication method supports will be used.
                        ctx.ProtocolMessage.UiLocales = "sv nb";
                        return Task.CompletedTask;
                    }
                }
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}