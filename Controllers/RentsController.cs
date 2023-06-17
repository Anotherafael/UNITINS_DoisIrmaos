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
    public class RentsController : Controller
    {
        private readonly Context _context;

        public RentsController(Context context)
        {
            _context = context;
        }

        // GET: Rents
        public async Task<IActionResult> Index()
        {
            var context = _context.Rents.Include(r => r.Buyer).Include(r => r.Category).Include(r => r.Driver).Include(r => r.Employee).Include(r => r.Protection).Include(r => r.Vehicle);
            return View(await context.ToListAsync());
        }

        // GET: Rents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rents == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                .Include(r => r.Buyer)
                .Include(r => r.Category)
                .Include(r => r.Driver)
                .Include(r => r.Employee)
                .Include(r => r.Protection)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Rents/Create
        public IActionResult Create()
        {
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Id");
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Id");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Id");
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,StartAt,EndAt,TakenAt,ReturnedAt,CategoryID,VehicleID,BuyerID,DriverID,EmployeeID,ProtectionID")] Rent rent)
        {

            if (rent.EndAt <= rent.StartAt)
            {
                ModelState.AddModelError("", "Ending date can't be sooner than the start date.");
                return View(rent);
            }

            if (rent.ReturnedAt <= rent.TakenAt)
            {
                ModelState.AddModelError("", "Return date can't be sooner than the date of taking.");
                return View(rent);
            }

            if (rent.Price < 0)
            {
                ModelState.AddModelError("", "Price can't be lower than 0.");
                return View(rent);
            }

            if (ModelState.IsValid)
            {
                _context.Add(rent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Id", rent.BuyerID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Id", rent.CategoryID);
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Id", rent.DriverID);
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Id", rent.EmployeeID);
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Id", rent.ProtectionID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Id", rent.VehicleID);
            return View(rent);
        }

        // GET: Rents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rents == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Id", rent.BuyerID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Id", rent.CategoryID);
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Id", rent.DriverID);
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Id", rent.EmployeeID);
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Id", rent.ProtectionID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Id", rent.VehicleID);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,StartAt,EndAt,TakenAt,ReturnedAt,CategoryID,VehicleID,BuyerID,DriverID,EmployeeID,ProtectionID")] Rent rent)
        {
            if (id != rent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.Id))
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
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Id", rent.BuyerID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Id", rent.CategoryID);
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Id", rent.DriverID);
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Id", rent.EmployeeID);
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Id", rent.ProtectionID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Id", rent.VehicleID);
            return View(rent);
        }

        // GET: Rents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rents == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                .Include(r => r.Buyer)
                .Include(r => r.Category)
                .Include(r => r.Driver)
                .Include(r => r.Employee)
                .Include(r => r.Protection)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rents == null)
            {
                return Problem("Entity set 'Context.Rents'  is null.");
            }
            var rent = await _context.Rents.FindAsync(id);
            if (rent != null)
            {
                _context.Rents.Remove(rent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
          return _context.Rents.Any(e => e.Id == id);
        }
    }
}
