using Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using log4net.Config;
using log4net.Core;
using log4net;
using System.Reflection;

namespace Portal.Controllers
{

    [Authorize]
    public class StoreController : Controller
    {
        private ProductDbContext DbContext;

        public StoreController(ProductDbContext ctx) => DbContext = ctx;

        public IActionResult Index() => View(DbContext.Products);
    }
}

