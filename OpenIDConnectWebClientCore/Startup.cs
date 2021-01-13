using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Threading.Tasks;

namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddControllersWithViews(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}