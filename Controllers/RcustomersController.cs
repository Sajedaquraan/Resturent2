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
    public class RcustomersController : Controller
    {
        private readonly ModelContext _context;

        public RcustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rcustomers
        public async Task<IActionResult> Index()
        {
              return _context.Rcustomers != null ? 
                          View(await _context.Rcustomers.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Rcustomers'  is null.");
        }

        // GET: Rcustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Rcustomers == null)
            {
                return NotFound();
            }

            var rcustomer = await _context.Rcustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rcustomer == null)
            {
                return NotFound();
            }

            return View(rcustomer);
        }

        // GET: Rcustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rcustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,ImagePath")] Rcustomer rcustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rcustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rcustomer);
        }

        // GET: Rcustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Rcustomers == null)
            {
                return NotFound();
            }

            var rcustomer = await _context.Rcustomers.FindAsync(id);
            if (rcustomer == null)
            {
                return NotFound();
            }
            return View(rcustomer);
        }

        // POST: Rcustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fname,Lname,ImagePath")] Rcustomer rcustomer)
        {
            if (id != rcustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rcustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RcustomerExists(rcustomer.Id))
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
            return View(rcustomer);
        }

        // GET: Rcustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Rcustomers == null)
            {
                return NotFound();
            }

            var rcustomer = await _context.Rcustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rcustomer == null)
            {
                return NotFound();
            }

            return View(rcustomer);
        }

        // POST: Rcustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Rcustomers == null)
            {
                return Problem("Entity set 'ModelContext.Rcustomers'  is null.");
            }
            var rcustomer = await _context.Rcustomers.FindAsync(id);
            if (rcustomer != null)
            {
                _context.Rcustomers.Remove(rcustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RcustomerExists(decimal id)
        {
          return (_context.Rcustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
