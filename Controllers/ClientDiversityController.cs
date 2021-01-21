using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traders.Data;
using Traders.Models;

namespace Traders.Controllers
{
    [Authorize(Roles = "Trader")]
    public class ClientDiversityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientDiversityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientDiversity
        public async Task<IActionResult> Index()
        {
            return View(await _context.clientDiversities.ToListAsync());
        }

        // GET: ClientDiversity/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientDiversityViewModel clientDiversityViewModel)
        {
            if (ModelState.IsValid)
            {
                clientDiversityViewModel.Id = Guid.NewGuid();
                _context.Add(clientDiversityViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientDiversityViewModel);
        }

        // GET: ClientDiversity/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ClientDiversityViewModelExists((Guid)id))
            {
                var clientDiversityViewModel = await _context.clientDiversities.FindAsync(id);
                return View(clientDiversityViewModel);
            }
            else
            {
                return NotFound();
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClientDiversityViewModel clientDiversityViewModel)
        {
            if (!ClientDiversityViewModelExists(clientDiversityViewModel.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientDiversityViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientDiversityViewModelExists(clientDiversityViewModel.Id))
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
            return View(clientDiversityViewModel);
        }

        private bool ClientDiversityViewModelExists(Guid id)
        {
            return _context.clientDiversities.Any(e => e.Id == id);
        }
    }
}
