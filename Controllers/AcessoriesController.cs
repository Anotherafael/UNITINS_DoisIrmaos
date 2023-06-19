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
    public class AcessoriesController : Controller
    {
        private readonly Context _context;

        public AcessoriesController(Context context)
        {
            _context = context;
        }

        // GET: Acessories
        public async Task<IActionResult> Index()
        {
              return View(await _context.Acessories.ToListAsync());
        }

        // GET: Acessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Acessories == null)
            {
                return NotFound();
            }

            var acessory = await _context.Acessories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessory == null)
            {
                return NotFound();
            }

            return View(acessory);
        }

        // GET: Acessories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Acessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Active")] Acessory acessory)
        {
            if (ModelState.IsValid)
            {
                acessory.Active = true;
                _context.Add(acessory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(acessory);
        }

        // GET: Acessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Acessories == null)
            {
                return NotFound();
            }

            var acessory = await _context.Acessories.FindAsync(id);
            if (acessory == null)
            {
                return NotFound();
            }
            return View(acessory);
        }

        // POST: Acessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Active")] Acessory acessory)
        {
            if (id != acessory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acessory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcessoryExists(acessory.Id))
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
            return View(acessory);
        }

        // GET: Acessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Acessories == null)
            {
                return NotFound();
            }

            var acessory = await _context.Acessories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessory == null)
            {
                return NotFound();
            }

            return View(acessory);
        }

        // POST: Acessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Acessories == null)
            {
                return Problem("Entity set 'Context.Acessories'  is null.");
            }
            var acessory = await _context.Acessories.FindAsync(id);
            if (acessory != null)
            {
                _context.Acessories.Remove(acessory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcessoryExists(int id)
        {
          return _context.Acessories.Any(e => e.Id == id);
        }
    }
}
