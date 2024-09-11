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
    public class RrolesController : Controller
    {
        private readonly ModelContext _context;

        public RrolesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rroles
        public async Task<IActionResult> Index()
        {
              return _context.Rroles != null ? 
                          View(await _context.Rroles.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Rroles'  is null.");
        }

        // GET: Rroles/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Rroles == null)
            {
                return NotFound();
            }

            var rrole = await _context.Rroles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rrole == null)
            {
                return NotFound();
            }

            return View(rrole);
        }

        // GET: Rroles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rroles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rolename")] Rrole rrole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rrole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rrole);
        }

        // GET: Rroles/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Rroles == null)
            {
                return NotFound();
            }

            var rrole = await _context.Rroles.FindAsync(id);
            if (rrole == null)
            {
                return NotFound();
            }
            return View(rrole);
        }

        // POST: Rroles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Rolename")] Rrole rrole)
        {
            if (id != rrole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rrole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RroleExists(rrole.Id))
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
            return View(rrole);
        }

        // GET: Rroles/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Rroles == null)
            {
                return NotFound();
            }

            var rrole = await _context.Rroles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rrole == null)
            {
                return NotFound();
            }

            return View(rrole);
        }

        // POST: Rroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Rroles == null)
            {
                return Problem("Entity set 'ModelContext.Rroles'  is null.");
            }
            var rrole = await _context.Rroles.FindAsync(id);
            if (rrole != null)
            {
                _context.Rroles.Remove(rrole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RroleExists(decimal id)
        {
          return (_context.Rroles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
