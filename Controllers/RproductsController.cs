using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resturent2.Models;

namespace Resturent2.Controllers
{
    public class RproductsController : Controller
    {
        private readonly ModelContext _context;

        public RproductsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rproducts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Rproducts.Include(r => r.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: Rproducts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Rproducts == null)
            {
                return NotFound();
            }

            var rproduct = await _context.Rproducts
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rproduct == null)
            {
                return NotFound();
            }

            return View(rproduct);
        }

        // GET: Rproducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Rcategories, "Id", "Id");
            return View();
        }

        // POST: Rproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Namee,Sale,Price,CategoryId")] Rproduct rproduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rproduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Rcategories, "Id", "Id", rproduct.CategoryId);
            return View(rproduct);
        }

        // GET: Rproducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Rproducts == null)
            {
                return NotFound();
            }

            var rproduct = await _context.Rproducts.FindAsync(id);
            if (rproduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Rcategories, "Id", "Id", rproduct.CategoryId);
            return View(rproduct);
        }

        // POST: Rproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Namee,Sale,Price,CategoryId")] Rproduct rproduct)
        {
            if (id != rproduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RproductExists(rproduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Rcategories, "Id", "Id", rproduct.CategoryId);
            return View(rproduct);
        }

        // GET: Rproducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Rproducts == null)
            {
                return NotFound();
            }

            var rproduct = await _context.Rproducts
                .Include(r => r.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rproduct == null)
            {
                return NotFound();
            }

            return View(rproduct);
        }

        // POST: Rproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Rproducts == null)
            {
                return Problem("Entity set 'ModelContext.Rproducts'  is null.");
            }
            var rproduct = await _context.Rproducts.FindAsync(id);
            if (rproduct != null)
            {
                _context.Rproducts.Remove(rproduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RproductExists(decimal id)
        {
          return (_context.Rproducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
