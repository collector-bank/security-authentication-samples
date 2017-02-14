using System.Security.Claims;
using System.Web.Mvc;

namespace CollectorBank.Security.Authentication.Samples.WsFederationOwin.Controllers
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