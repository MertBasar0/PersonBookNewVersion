using Microsoft.AspNetCore.Mvc;

namespace AuthenticationServer.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
