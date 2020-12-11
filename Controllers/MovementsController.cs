using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Traders.Data;
using Traders.Models;

namespace Traders.Controllers
{
    public class MovementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movements.ToListAsync());
        }

        // GET: Movements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementsViewModel = await _context.Movements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movementsViewModel == null)
            {
                return NotFound();
            }

            return View(movementsViewModel);
        }

        // GET: Movements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateMov,UserGuid")] MovementsViewModel movementsViewModel)
        {
            if (ModelState.IsValid)
            {
                movementsViewModel.Id = Guid.NewGuid();
                _context.Add(movementsViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movementsViewModel);
        }

        // GET: Movements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementsViewModel = await _context.Movements.FindAsync(id);
            if (movementsViewModel == null)
            {
                return NotFound();
            }
            return View(movementsViewModel);
        }

        // POST: Movements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateMov,UserGuid")] MovementsViewModel movementsViewModel)
        {
            if (id != movementsViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movementsViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovementsViewModelExists(movementsViewModel.Id))
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
            return View(movementsViewModel);
        }

        // GET: Movements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementsViewModel = await _context.Movements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movementsViewModel == null)
            {
                return NotFound();
            }

            return View(movementsViewModel);
        }

        // POST: Movements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var movementsViewModel = await _context.Movements.FindAsync(id);
            _context.Movements.Remove(movementsViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovementsViewModelExists(Guid id)
        {
            return _context.Movements.Any(e => e.Id == id);
        }
    }
}
