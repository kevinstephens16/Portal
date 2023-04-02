using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
