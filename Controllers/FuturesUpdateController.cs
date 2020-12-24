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
    [Authorize(Roles = "Trader")]
    public class FuturesUpdateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuturesUpdateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FuturesUpdate
        public async Task<IActionResult> Index()
        {
            return View(await _context.FuturesUpdates.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModifDate,Gain")] FuturesUpdateViewModel futuresUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                futuresUpdateViewModel.Id = Guid.NewGuid();
                _context.Add(futuresUpdateViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(futuresUpdateViewModel);
        }

        private bool FuturesUpdateViewModelExists(Guid id)
        {
            return _context.FuturesUpdates.Any(e => e.Id == id);
        }
    }
}
