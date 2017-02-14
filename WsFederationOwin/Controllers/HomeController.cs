using System.Web.Mvc;

namespace CollectorBank.Security.Authentication.Samples.WsFederationOwin.Controllers
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