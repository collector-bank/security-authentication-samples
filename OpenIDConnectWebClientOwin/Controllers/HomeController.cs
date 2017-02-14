using System.Web.Mvc;

namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientOwin.Controllers
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