using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UNITINS_DoisIrmaos.DAL;
using UNITINS_DoisIrmaos.Models;

namespace UNITINS_DoisIrmaos.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly Context _context;

        public CategoriesController(Context context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'Context.Courses'  is null.");
            }

            var category = from m in _context.Categories
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                category = category.Where(s => s.Name!.Contains(searchString)).Include(s => s.Features);
            }
            
            return View(await category.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var catfeats = _context.CategoryFeatures.Where(f => f.CategoryID == category.Id).ToList();
            var featuresNames = new List<String>();
            foreach (var item in catfeats)
            {
                var feature = await _context.Features.FindAsync(item.FeatureID);
                featuresNames.Add(feature.Name);
            }

            ViewBag.Features = featuresNames;
            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewBag.Features = new MultiSelectList(_context.Features, "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category, List<int> Features)
        {
            if (ModelState.IsValid)
            {

                
                _context.Add(category);
                await _context.SaveChangesAsync();

                CategoryFeature catfeat = new CategoryFeature();

                var cat = _context.Categories.Where(x => x.Name == category.Name).FirstOrDefault();
                category = await _context.Categories.FindAsync(cat.Id);
                catfeat.CategoryID = category.Id;

                for (int i = 0; i < Features.Count; i++)
                {
                    var feat = await _context.Features.FindAsync(Features[i]);
                    catfeat.FeatureID = feat.Id;
                    _context.Add(catfeat);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Features = new MultiSelectList(_context.Features, "Id", "Name");
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'Context.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _context.Categories.Any(e => e.Id == id);
        }
    }
}
