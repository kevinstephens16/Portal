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

namespace Portal.Controllers
{
    [Authorize]
    public class DatatablesController : Controller
    {
        private ProductDbContext _context;

        public DatatablesController(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddEdit(int id = 0)
        {
            if (id == 0)
                return View(new Product());
            else
            {
                var item = await _context.Products.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return View(item);
            }
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Product());
            else
            {
                var item = await _context.Products.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return View(item);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(int id, [Bind("Id, Name, Category, Price")] Product item)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _context.Add(item);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(item.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Products.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEdit", item) });
        }

        [NoDirectAccess]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [Bind("Id")] Product item)
        {
            try
            {
                _context.Products.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(item.Id))
                { return NotFound(); }
                else
                { throw; }
            }
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Products.ToList()) });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}