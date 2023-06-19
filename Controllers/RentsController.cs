using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
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

            var rentAcessories = _context.RentAcessories.Where(x => x.RentID == id).ToList();
            var acessoryNames = new List<String>();
            foreach (var item in rentAcessories)
            {
                var acessory = await _context.Acessories.FindAsync(item.AcessoryID);
                acessoryNames.Add(acessory.Name);
            }

            ViewBag.Acessories = acessoryNames;

            var rentTaxes = _context.RentTaxes.Where(x => x.RentID == id).ToList();
            var taxNames = new List<String>();
            foreach (var item in rentTaxes)
            {
                var tax = await _context.Taxes.FindAsync(item.TaxID);
                taxNames.Add(tax.Name);
            }

            ViewBag.Taxes = taxNames;
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
            var AvailableVehicles = _context.Vehicles.Where(r => r.Available == true);
            ViewData["VehicleID"] = new SelectList(AvailableVehicles, "Id", "Name");
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
            if (rent.StartAt <= DateTime.Now)
            {
                ModelState.AddModelError("", "Start date can't be sooner than currently time.");
                loadData(rent);
                return View(rent);
            }
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
            var AvailableVehiclesFromCategory = _context.Vehicles.Where(r => r.Available == true && r.Active == true && r.CategoryID == rent.CategoryID);
            ViewData["VehicleID"] = new SelectList(AvailableVehiclesFromCategory, "Id", "Name");
            ViewData["Acessories"] = new MultiSelectList(_context.Acessories, "Id", "Name", rent.Acessories);
            ViewData["Taxes"] = new MultiSelectList(_context.Taxes, "Id", "Name", rent.Taxes);
            ViewBag.Price = rent.Price;
        }

        // GET: Rents/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            loadData(rent);
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartAt,EndAt,TakenAt,ReturnedAt,BuyerID,CategoryID,VehicleID,DriverID,EmployeeID,ProtectionID")] Rent rent, List<int> Acessories, List<int> Taxes)
        {
            if (id != rent.Id)
            {
                return NotFound();
            }

            if (rent.StartAt <= DateTime.Now)
            {
                ModelState.AddModelError("", "Start date can't be sooner than currently time.");
                loadData(rent);
                return View(rent);
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

            //if (rent.Price < 0)
            //{
            //    ModelState.AddModelError("", "Price can't be lower than 0.");
            //    loadData(rent);
            //    return View(rent);
            //}

            if (ModelState.IsValid)
            {
                try
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

                    var rentAcessories = _context.RentAcessories.Where(a => a.RentID == rent.Id).ToList();

                    foreach (var acessory in rentAcessories)
                    {
                        _context.Remove(acessory);
                    }

                    RentAcessory rentAcessory = new RentAcessory();
                    rentAcessory.RentID = rent.Id;

                    foreach (var acessoryID in Acessories)
                    {
                        rentAcessory.AcessoryID = acessoryID;
                        var acessory = await _context.Acessories.FirstOrDefaultAsync(x => x.Id == acessoryID);
                        totalPrice += acessory.Price;
                        _context.Add(rentAcessory);
                        await _context.SaveChangesAsync();
                    }

                    _context.Update(rent);

                    var rentTaxes = _context.RentTaxes.Where(t => t.RentID == rent.Id).ToList();

                    foreach (var taxes in rentTaxes)
                    {
                        _context.Remove(taxes);
                    }

                    RentTax rentTax = new RentTax();
                    rentTax.RentID = rent.Id;

                    foreach(var taxID in Taxes) {
                        rentTax.TaxID = taxID;
                        var tax = await _context.Taxes.FirstOrDefaultAsync(x => x.Id == taxID);
                        totalPrice += ((float)tax.PricePerDay); 
                        _context.Add(rentTax);
                        await _context.SaveChangesAsync();
                    }

                    rent.Price = totalPrice;
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
            loadData(rent);
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
