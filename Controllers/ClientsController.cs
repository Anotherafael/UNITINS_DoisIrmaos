﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UNITINS_DoisIrmaos.DAL;
using UNITINS_DoisIrmaos.Models;

namespace UNITINS_DoisIrmaos.Controllers
{
    public class ClientsController : Controller
    {
        private readonly Context _context;

        public ClientsController(Context context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
              return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,Cnh,BirthDate")] Client client)
        {
            if (client.BirthDate > DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("", "Invalid Birth Day.");
                return View(client);
            }
            if (client.PhoneNumber.Length < 13)
            {
                ModelState.AddModelError("", "Invalid Phone.");
                return View(client);
            }
            if (client.Cnh.Length < 11)
            {
                ModelState.AddModelError("", "Invalid CNH.");
                return View(client);
            }

            if (ModelState.IsValid)
            {
                client.Active = true;
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", client);
        }

        public IActionResult CreateFull()
        {
            return View("CreateFull");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFull([Bind("Id,Name,Email,PhoneNumber,Cnh,BirthDate,Password,Address")] Client client, string ConfirmPassword)
        {

            if (client.BirthDate > DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("", "Invalid Birth Day.");
                return View(client);
            }
            if (client.PhoneNumber.Length < 13)
            {
                ModelState.AddModelError("", "Invalid Phone.");
                return View(client);
            }
            if (client.Cnh.Length < 11)
            {
                ModelState.AddModelError("", "Invalid CNH.");
                return View(client);
            }
            if (!client.Password.Equals(ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords don't match.");
                return View(client);
            }
            if (ModelState.IsValid)
            {
                client.Active = true;
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("CreateFull", client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View("Edit", client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,BirthDate,Cnh,Password,PhoneNumber,Address")] Client client, string ConfirmPassword)
        {
            if (id != client.Id)
            {
                return NotFound();
            }
            if (client.Password.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Insert your password.");
                return View(client);
            }
            if (!client.Password.Equals(ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords don't match.");
                return View(client);
            }
            if (client.PhoneNumber.Length < 13)
            {
                ModelState.AddModelError("", "Invalid Phone.");
                return View(client);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.Active = true;
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'Context.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return _context.Clients.Any(e => e.Id == id);
        }
    }
}
