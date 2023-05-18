using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Core;
using static Helper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using Portal.Repository;

namespace Portal.Controllers
{
    //[Authorize]
    public class DevExtremeController : Controller
    {
        private ProductDbContext _context;

        public DevExtremeController(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData(DataSourceLoadOptions loadOptions)
        {
            var source = _context.Products.Select(o => new {
                o.Id,
                o.Name,
                o.Category,
                o.Price
            });

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));
        }

        [HttpGet("Products")]
        public async Task<IActionResult> Products(DataSourceLoadOptions loadOptions)
        {
            var source = _context.Products.Select(o => new {
                o.Id,
                o.Name,
                o.Category,
                o.Price
            });

            return Json(await DataSourceLoader.LoadAsync(source, loadOptions));
        }
        [HttpGet]
        public ContentResult GetProduct()
        {
            List<Product> product1 = new List<Product>();
            var productlist = _context.Products.ToList();
            productlist = (from x in productlist select x).ToList();
            foreach (Product product in productlist)
            {
                Product productmodel = new Product();
                productmodel.Id = product.Id;
                productmodel.Name = product.Name;
                productmodel.Category = product.Category;
                productmodel.Price = product.Price;
                product1.Add(productmodel);
            }
            return Content(JsonConvert.SerializeObject(product1), "application/json");
        }
        //public async Task<IActionResult> GetProduct()
        //{
        //    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Products.ToList()) });
        //}
    }
}