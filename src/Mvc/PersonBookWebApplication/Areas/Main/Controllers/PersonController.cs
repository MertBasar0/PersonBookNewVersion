using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonBookWebApplication.Areas.Main.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
    [Area("Main")]
    public class PersonController : Controller
    {
        [HttpGet]
        [Route("Main/[controller]/[action]", Name = "apolet")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
