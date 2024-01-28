using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonBookWebApplication.Areas.Main.Controllers
{
    [Area("Main")]
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
