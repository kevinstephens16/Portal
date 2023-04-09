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

namespace Portal.Controllers
{
    public class DatatablesController : Controller
    {
        private ProductDbContext _context;

        public DatatablesController(ProductDbContext context)
        {
            _context = context;
        }

        //public DatatablesController(ProductDbContext ctx) => DbContext = ctx;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            //var data = _context.Set<Product>().AsQueryable();

            //get total count of data in table
            //totalRecord = data.Count();

            // search data when search value found
            //if (!string.IsNullOrEmpty(searchValue))
            //{
            //    data = data.Where(x =>
            //      x.FirstName.ToLower().Contains(searchValue.ToLower())
            //      || x.LastName.ToLower().Contains(searchValue.ToLower())
            //      || x.Designation.ToLower().Contains(searchValue.ToLower())
            //      || x.Salary.ToString().ToLower().Contains(searchValue.ToLower())

            //    );
            //}
            var customerData = (from tempcustomer in _context.Products
                                select tempcustomer);
            // get total count of records after search 
            //filterRecord = data.Count();

            //sort data
            //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            //    data = data.OrderBy(sortColumn + " " + sortColumnDirection);

            totalRecord = customerData.Count();
            filterRecord = customerData.Count();

            //pagination
            var data = customerData.Skip(skip).Take(pageSize).ToList();

            var returnObj = new { draw = draw, data = data, recordsTotal = totalRecord, recordsFiltered = filterRecord };
            return Json(returnObj);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index", "Datatables");
                }

                return View("Edit");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpPost]
        //public IActionResult Delete(string id)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(id))
        //        {
        //            return RedirectToAction("Index", "Datatables");
        //        }

        //        int result = 0;

        //        if (result > 0)
        //        {
        //            return Json(data: true);
        //        }
        //        else
        //        {
        //            return Json(data: false);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public ActionResult Create()
        {
            var model = new Product();
            return View("_Create", model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product productVM)
        {

            if (!ModelState.IsValid)
                return View("_Create", productVM);

            Product product = MapToViewModel(productVM);

            _context.Products.Add(product);
            var task = _context.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to add the Product");
                return View("_Create", productVM);
            }

            return Content("success");
        }

        public ActionResult Edit(int id)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            Product productViewModel = MapToViewModel(product);

            if (isAjax)
                return PartialView("_Edit", productViewModel);
            return View(productViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product productVM)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(isAjax ? "_Edit" : "Edit", productVM);
            }

            Product product = MapToViewModel(productVM);

            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            var task = _context.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to update the Product");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(isAjax ? "_Edit" : "Edit", productVM);
            }

            if (isAjax)
            {
                return Content("success");
            }

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            Product productViewModel = MapToViewModel(product);

            if (isAjax)
                return PartialView("_Delete", product);
            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            var product = new Product { Id = id };
            _context.Products.Attach(product);
            _context.Products.Remove(product);

            var task = _context.SaveChangesAsync();
            await task;

            if (task.Exception != null)
            {
                ModelState.AddModelError("", "Unable to Delete the Product");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Product productVM = MapToViewModel(product);
                return View(isAjax ? "_Delete" : "Delete", productVM);
            }

            if (isAjax)
            {
                return Content("success");
            }

            return RedirectToAction("Index");

        }

        private Product MapToViewModel(Product product)
        {

            Product productViewModel = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            };

            return productViewModel;
        }

    }
}