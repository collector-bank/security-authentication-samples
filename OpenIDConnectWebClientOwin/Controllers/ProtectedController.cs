using System.Security.Claims;
using System.Web.Mvc;

namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientOwin.Controllers
{

    [Authorize]
    [RequireHttps]
    public class ProtectedController : Controller
    {

        public ActionResult Index()
        {
            return View(User as ClaimsPrincipal);
        }
    }
}