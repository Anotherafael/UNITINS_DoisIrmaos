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
    public class ProtectionsController : Controller
    {
        private readonly Context _context;

        public ProtectionsController(Context context)
        {
            _context = context;
        }

        // GET: Protections
        public async Task<IActionResult> Index()
        {
              return View(await _context.Protections.ToListAsync());
        }

        // GET: Protections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Protections == null)
            {
                return NotFound();
            }

            var protection = await _context.Protections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (protection == null)
            {
                return NotFound();
            }

            return View(protection);
        }

        // GET: Protections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Protections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,PricePerDay")] Protection protection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(protection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(protection);
        }

        // GET: Protections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Protections == null)
            {
                return NotFound();
            }

            var protection = await _context.Protections.FindAsync(id);
            if (protection == null)
            {
                return NotFound();
            }
            return View(protection);
        }

        // POST: Protections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,PricePerDay")] Protection protection)
        {
            if (id != protection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(protection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtectionExists(protection.Id))
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
            return View(protection);
        }

        // GET: Protections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Protections == null)
            {
                return NotFound();
            }

            var protection = await _context.Protections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (protection == null)
            {
                return NotFound();
            }

            return View(protection);
        }

        // POST: Protections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Protections == null)
            {
                return Problem("Entity set 'Context.Protections'  is null.");
            }
            var protection = await _context.Protections.FindAsync(id);
            if (protection != null)
            {
                _context.Protections.Remove(protection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProtectionExists(int id)
        {
          return _context.Protections.Any(e => e.Id == id);
        }
    }
}
