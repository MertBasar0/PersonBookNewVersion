using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonBookWebApplication.Areas.Main.Controllers
{

    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
