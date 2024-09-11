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
    public class RcategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;


        public RcategoriesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }

        // GET: Rcategories
        public async Task<IActionResult> Index()
        {
            return _context.Rcategories != null ?
                        View(await _context.Rcategories.ToListAsync()) :
                        Problem("Entity set 'ModelContext.Rcategories'  is null.");
        }

        // GET: Rcategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Rcategories == null)
            {
                return NotFound();
            }

            var rcategory = await _context.Rcategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rcategory == null)
            {
                return NotFound();
            }

            return View(rcategory);
        }

        // GET: Rcategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,ImageFile")] Rcategory rcategory)
        {
            if (ModelState.IsValid)
            {

                if (rcategory.ImageFile != null)
                {
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString()
                                      + "_"
                                      + rcategory.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);


                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await rcategory.ImageFile.CopyToAsync(fileStream);
                    }

                    rcategory.ImagePath = fileName;
                }


                _context.Add(rcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rcategory);
        }

        // GET: Rcategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Rcategories == null)
            {
                return NotFound();
            }

            var rcategory = await _context.Rcategories.FindAsync(id);
            if (rcategory == null)
            {
                return NotFound();
            }
            return View(rcategory);
        }

        // POST: Rcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,ImageFile")] Rcategory rcategory)
        {
            if (id != rcategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


                try
                {

                    if (rcategory.ImageFile != null)
                    {
                        string wwwRootPath = _environment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString()
                                          + "_"
                                          + rcategory.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);


                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await rcategory.ImageFile.CopyToAsync(fileStream);
                        }

                        rcategory.ImagePath = fileName;
                    }

                    _context.Update(rcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RcategoryExists(rcategory.Id))
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
            return View(rcategory);
        }

        // GET: Rcategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Rcategories == null)
            {
                return NotFound();
            }

            var rcategory = await _context.Rcategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rcategory == null)
            {
                return NotFound();
            }

            return View(rcategory);
        }

        // POST: Rcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Rcategories == null)
            {
                return Problem("Entity set 'ModelContext.Rcategories'  is null.");
            }
            var rcategory = await _context.Rcategories.FindAsync(id);
            if (rcategory != null)
            {
                _context.Rcategories.Remove(rcategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RcategoryExists(decimal id)
        {
            return (_context.Rcategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}