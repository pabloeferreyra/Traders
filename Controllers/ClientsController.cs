using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Traders.Data;
using Traders.Models;

namespace Traders.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Trader")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }


        [Authorize(Roles = "Trader, Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.ClientUCien = await _context.Clients.OrderBy(c => c.Code).Where(c => c.Code < 100).Select(c => c.Code).LastOrDefaultAsync();
            var cCode = await _context.Clients.OrderBy(c => c.Code).Select(c => c.Code).LastOrDefaultAsync();
            if (cCode != 0 && cCode > 100)
                ViewBag.ClientCode = cCode + 1;
            else
                ViewBag.ClientCode = 100;
            return View();
        }

        [Authorize(Roles = "Trader, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Email")] ClientsViewModel clientsViewModel)
        {
            if (ModelState.IsValid)
            {
                clientsViewModel.Id = Guid.NewGuid();
                _context.Add(clientsViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientsViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsViewModel = await _context.Clients.FindAsync(id);
            if (clientsViewModel == null)
            {
                return NotFound();
            }
            return View(clientsViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Code,Email")] ClientsViewModel clientsViewModel)
        {
            if (id != clientsViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientsViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsViewModelExists(clientsViewModel.Id))
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
            return View(clientsViewModel);
        }

        private bool ClientsViewModelExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
