using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonBookWebApplication.Areas.Main.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "admin")]
    [Area(areaName: "Main")]
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
