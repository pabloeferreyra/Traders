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
    [Authorize(Roles = "Admin")]
    public class BadgesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BadgesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Badges
        public async Task<IActionResult> Index()
        {
            return View(await _context.Badges.ToListAsync());
        }

        // GET: Badges/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var badgesViewModel = await _context.Badges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (badgesViewModel == null)
            {
                return NotFound();
            }

            return View(badgesViewModel);
        }

        // GET: Badges/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Badges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BadgesViewModel badgesViewModel)
        {
            if (ModelState.IsValid)
            {
                badgesViewModel.Id = Guid.NewGuid();
                _context.Add(badgesViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(badgesViewModel);
        }

        // POST: Badges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var badgesViewModel = await _context.Badges.FindAsync(id);
            _context.Badges.Remove(badgesViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BadgesViewModelExists(Guid id)
        {
            return _context.Badges.Any(e => e.Id == id);
        }
    }
}
