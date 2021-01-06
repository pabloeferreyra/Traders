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
    [Authorize(Roles = "Trader, Admin")]
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

        // GET: Badges/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BadgesViewModel badgesViewModel)
        {
            if (ModelState.IsValid && !BadgesViewModelExists(badgesViewModel.Name))
            {
                badgesViewModel.Id = Guid.NewGuid();
                _context.Add(badgesViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(badgesViewModel);
        }

        private bool BadgesViewModelExists(string name)
        {
            return _context.Badges.Any(e => e.Name.ToUpper() == name.ToUpper());
        }
    }
}
