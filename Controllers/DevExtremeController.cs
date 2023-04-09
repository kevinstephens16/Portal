using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    public class DevExtremeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
