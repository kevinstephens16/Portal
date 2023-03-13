using Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Portal.Controllers {

    [Authorize]
    public class StoreController : Controller {
        private ProductDbContext DbContext;

        public StoreController(ProductDbContext ctx) => DbContext = ctx;

        public IActionResult Index() => View(DbContext.Products);
    }
}
