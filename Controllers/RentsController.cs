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
            var context = _context.Rents
                .Include(r => r.Buyer)
                .Include(r => r.Category)
                .Include(r => r.Driver)
                .Include(r => r.Employee)
                .Include(r => r.Protection)
                .Include(r => r.Vehicle)
                .Include(r => r.Acessories)
                .Include(r => r.Taxes);
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
                .Include(r => r.Acessories)
                .Include(r => r.Taxes)
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
            loadData();
            return View();
        }

        public void loadData()
        {
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Name");
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Name");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Name");
            ViewData["Acessories"] = new MultiSelectList(_context.Acessories, "Id", "Name");
            ViewData["Taxes"] = new MultiSelectList(_context.Taxes, "Id", "Name");
            ViewBag.Price = 0.0;
        }

        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,StartAt,EndAt,TakenAt,ReturnedAt,CategoryID,VehicleID,BuyerID,DriverID,EmployeeID,ProtectionID")] Rent rent, List<int> Acessories, List<int> Taxes)
        {

            if (rent.EndAt <= rent.StartAt)
            {
                ModelState.AddModelError("", "Ending date can't be sooner than the start date.");
                loadData(rent);
                return View(rent);
            }

            //if (rent.ReturnedAt != null && rent.TakenAt !=null)
            //{
            //    if (rent.ReturnedAt <= rent.TakenAt)
            //    {
            //        ModelState.AddModelError("", "Return date can't be sooner than the date of taking.");
            //        loadData(rent);
            //        return View(rent);
            //    }
            //}

            //if (rent.Price < 0)
            //{
            //    ModelState.AddModelError("", "Price can't be lower than 0.");
            //    loadData(rent);
            //    return View(rent);
            //}

            if (ModelState.IsValid)
            {

                var totalPrice = 0F;
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == rent.CategoryID);
                totalPrice += category.Price;
                if (rent.ProtectionID != null)
                {
                    var protection = await _context.Protections.FirstOrDefaultAsync(x => x.Id == rent.ProtectionID);
                    totalPrice += protection.PricePerDay;
                }

                rent.Price = totalPrice;

                _context.Add(rent);
                await _context.SaveChangesAsync();

                //CategoryFeature catfeat = new CategoryFeature();

                //var cat = _context.Categories.Where(x => x.Name == category.Name).FirstOrDefault();
                //category = await _context.Categories.FindAsync(cat.Id);
                //catfeat.CategoryID = category.Id;

                //for (int i = 0; i < Features.Count; i++)
                //{
                //    var feat = await _context.Features.FindAsync(Features[i]);
                //    catfeat.FeatureID = feat.Id;
                //    _context.Add(catfeat);
                //    await _context.SaveChangesAsync();
                //}

                RentAcessory rentAcessory = new RentAcessory();
                rent = await _context.Rents.Where(x => (x.CategoryID == rent.CategoryID && x.BuyerID == rent.BuyerID && x.StartAt == rent.StartAt)).FirstOrDefaultAsync();
                rentAcessory.RentID = rent.Id;

                for (int i = 0; i < Acessories.Count; i++)
                {
                    var item = await _context.Acessories.FindAsync(Acessories[i]);
                    rent.Price += item.Price;
                    rentAcessory.AcessoryID = item.Id;
                    _context.Add(rentAcessory);
                    await _context.SaveChangesAsync();
                }

                _context.Update(rent);

                RentTax rentTax = new RentTax();
                rent = await _context.Rents.Where(x => (x.CategoryID == rent.CategoryID && x.BuyerID == rent.BuyerID && x.StartAt == rent.StartAt)).FirstOrDefaultAsync();
                rentTax.RentID = rent.Id;

                for (int i = 0; i < Taxes.Count; i++)
                {
                    var item = await _context.Taxes.FindAsync(Taxes[i]);
                    rent.Price += ((float)item.PricePerDay);
                    rentTax.TaxID = item.Id;
                    _context.Add(rentTax);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            loadData(rent);
            return View(rent);
        }

        public void loadData(Rent rent)
        {
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Name", rent.BuyerID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", rent.CategoryID);
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Name", rent.DriverID);
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Name", rent.EmployeeID);
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Name", rent.ProtectionID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Name", rent.VehicleID);
            ViewData["Acessories"] = new MultiSelectList(_context.Acessories, "Id", "Name", rent.Acessories);
            ViewData["Taxes"] = new MultiSelectList(_context.Taxes, "Id", "Name", rent.Taxes);
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
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Name", rent.BuyerID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", rent.CategoryID);
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Name", rent.DriverID);
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Name", rent.EmployeeID);
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Name", rent.ProtectionID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Name", rent.VehicleID);
            ViewData["Acessories"] = new MultiSelectList(_context.Acessories, "Id", "Name", rent.Acessories);
            ViewData["Taxes"] = new MultiSelectList(_context.Taxes, "Id", "Name", rent.Taxes);
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

            if (rent.EndAt <= rent.StartAt)
            {
                ModelState.AddModelError("", "Ending date can't be sooner than the start date.");
                loadData(rent);
                return View(rent);
            }

            //if (rent.ReturnedAt <= rent.EndAt)
            //{
            //    ModelState.AddModelError("", "Return date can't be sooner than the date of taking.");
            //    loadData(rent);
            //    return View(rent);
            //}

            if (rent.Price < 0)
            {
                ModelState.AddModelError("", "Price can't be lower than 0.");
                loadData(rent);
                return View(rent);
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
            ViewData["BuyerID"] = new SelectList(_context.Clients, "Id", "Name", rent.BuyerID);
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", rent.CategoryID);
            ViewData["DriverID"] = new SelectList(_context.Clients, "Id", "Name", rent.DriverID);
            ViewData["EmployeeID"] = new SelectList(_context.Personnel, "Id", "Name", rent.EmployeeID);
            ViewData["ProtectionID"] = new SelectList(_context.Protections, "Id", "Name", rent.ProtectionID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "Id", "Name", rent.VehicleID);
            ViewData["Acessories"] = new MultiSelectList(_context.Acessories, "Id", "Name", rent.Acessories);
            ViewData["Taxes"] = new MultiSelectList(_context.Taxes, "Id", "Name", rent.Taxes);
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
                .Include(r => r.Acessories)
                .Include(r => r.Taxes)
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
