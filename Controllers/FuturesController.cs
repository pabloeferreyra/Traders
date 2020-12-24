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
    public class FuturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Futures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Futures.Include(f => f.Client).Include(f => f.Participation);
            var futures = await applicationDbContext.ToListAsync();
            for (int f = 0; f < futures.Count(); f++)
            {
                futures[f].FinishDate = futures[f].StartDate.AddMonths(6);
            }
            return View(futures);
        }

        // GET: Futures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (FuturesViewModelExists((Guid)id))
            {
                var futuresViewModel = await _context.Futures
                .Include(f => f.Client)
                .Include(f => f.Participation)
                .FirstOrDefaultAsync(m => m.Id == id);
                futuresViewModel.FinishDate = futuresViewModel.StartDate.AddMonths(6);
                return View(futuresViewModel);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Code");
            ViewData["ParticipationId"] = new SelectList(_context.Participations, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,StartDate,ParticipationId,Capital")] FuturesViewModel futuresViewModel)
        {
            if (ModelState.IsValid)
            {
                futuresViewModel.Id = Guid.NewGuid();
                _context.Add(futuresViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Code", futuresViewModel.ClientId);
            ViewData["ParticipationId"] = new SelectList(_context.Participations, "Id", "Name", futuresViewModel.ParticipationId);
            return View(futuresViewModel);
        }

        private bool FuturesViewModelExists(Guid id)
        {
            return _context.Futures.Any(e => e.Id == id);
        }
    }
}
