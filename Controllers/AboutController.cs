using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
