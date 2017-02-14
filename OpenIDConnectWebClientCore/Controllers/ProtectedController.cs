using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectorBank.Security.Authentication.Samples.OpenIDConnectWebClientCore.Controllers
{

    [Authorize]
    [RequireHttps]
    public class ProtectedController : Controller
    {

        public ActionResult Index()
        {
            return View(User);
        }
    }
}