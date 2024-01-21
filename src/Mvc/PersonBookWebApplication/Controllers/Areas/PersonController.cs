using Microsoft.AspNetCore.Mvc;

namespace PersonBookWebApplication.Controllers.Areas
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
