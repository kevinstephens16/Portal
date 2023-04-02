using Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{

    public class HomeController : Controller
    {
        private ProductDbContext DbContext;

        public HomeController(ProductDbContext ctx) => DbContext = ctx;

        public IActionResult Index() => View(DbContext.Products);
    }
}
