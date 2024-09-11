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
    public class RproductcustomersController : Controller
    {
        private readonly ModelContext _context;

        public RproductcustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rproductcustomers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Rproductcustomers.Include(r => r.Customer).Include(r => r.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Rproductcustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Rproductcustomers == null)
            {
                return NotFound();
            }

            var rproductcustomer = await _context.Rproductcustomers
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rproductcustomer == null)
            {
                return NotFound();
            }

            return View(rproductcustomer);
        }

        // GET: Rproductcustomers/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Rproducts, "Id", "Id");
            return View();
        }

        // POST: Rproductcustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CustomerId,Quantity,DateFrom,DateTo")] Rproductcustomer rproductcustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rproductcustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id", rproductcustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Rproducts, "Id", "Id", rproductcustomer.ProductId);
            return View(rproductcustomer);
        }

        // GET: Rproductcustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Rproductcustomers == null)
            {
                return NotFound();
            }

            var rproductcustomer = await _context.Rproductcustomers.FindAsync(id);
            if (rproductcustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id", rproductcustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Rproducts, "Id", "Id", rproductcustomer.ProductId);
            return View(rproductcustomer);
        }

        // POST: Rproductcustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ProductId,CustomerId,Quantity,DateFrom,DateTo")] Rproductcustomer rproductcustomer)
        {
            if (id != rproductcustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rproductcustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RproductcustomerExists(rproductcustomer.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id", rproductcustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Rproducts, "Id", "Id", rproductcustomer.ProductId);
            return View(rproductcustomer);
        }

        // GET: Rproductcustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Rproductcustomers == null)
            {
                return NotFound();
            }

            var rproductcustomer = await _context.Rproductcustomers
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rproductcustomer == null)
            {
                return NotFound();
            }

            return View(rproductcustomer);
        }

        // POST: Rproductcustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Rproductcustomers == null)
            {
                return Problem("Entity set 'ModelContext.Rproductcustomers'  is null.");
            }
            var rproductcustomer = await _context.Rproductcustomers.FindAsync(id);
            if (rproductcustomer != null)
            {
                _context.Rproductcustomers.Remove(rproductcustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RproductcustomerExists(decimal id)
        {
          return (_context.Rproductcustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
