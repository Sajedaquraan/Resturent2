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
    public class RuserloginsController : Controller
    {
        private readonly ModelContext _context;

        public RuserloginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Ruserlogins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Ruserlogins.Include(r => r.Customer).Include(r => r.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Ruserlogins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Ruserlogins == null)
            {
                return NotFound();
            }

            var ruserlogin = await _context.Ruserlogins
                .Include(r => r.Customer)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ruserlogin == null)
            {
                return NotFound();
            }

            return View(ruserlogin);
        }

        // GET: Ruserlogins/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id");
            ViewData["RoleId"] = new SelectList(_context.Rroles, "Id", "Id");
            return View();
        }

        // POST: Ruserlogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Passwordd,RoleId,CustomerId")] Ruserlogin ruserlogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ruserlogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id", ruserlogin.CustomerId);
            ViewData["RoleId"] = new SelectList(_context.Rroles, "Id", "Id", ruserlogin.RoleId);
            return View(ruserlogin);
        }

        // GET: Ruserlogins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Ruserlogins == null)
            {
                return NotFound();
            }

            var ruserlogin = await _context.Ruserlogins.FindAsync(id);
            if (ruserlogin == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id", ruserlogin.CustomerId);
            ViewData["RoleId"] = new SelectList(_context.Rroles, "Id", "Id", ruserlogin.RoleId);
            return View(ruserlogin);
        }

        // POST: Ruserlogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,UserName,Passwordd,RoleId,CustomerId")] Ruserlogin ruserlogin)
        {
            if (id != ruserlogin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruserlogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RuserloginExists(ruserlogin.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Rcustomers, "Id", "Id", ruserlogin.CustomerId);
            ViewData["RoleId"] = new SelectList(_context.Rroles, "Id", "Id", ruserlogin.RoleId);
            return View(ruserlogin);
        }

        // GET: Ruserlogins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Ruserlogins == null)
            {
                return NotFound();
            }

            var ruserlogin = await _context.Ruserlogins
                .Include(r => r.Customer)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ruserlogin == null)
            {
                return NotFound();
            }

            return View(ruserlogin);
        }

        // POST: Ruserlogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Ruserlogins == null)
            {
                return Problem("Entity set 'ModelContext.Ruserlogins'  is null.");
            }
            var ruserlogin = await _context.Ruserlogins.FindAsync(id);
            if (ruserlogin != null)
            {
                _context.Ruserlogins.Remove(ruserlogin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RuserloginExists(decimal id)
        {
          return (_context.Ruserlogins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
