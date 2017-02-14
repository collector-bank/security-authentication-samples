using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientCore.Controllers
{

    [RequireHttps]
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}