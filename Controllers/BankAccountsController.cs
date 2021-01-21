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
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BankAccounts.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (BankAccountsViewModelExists((Guid)id))
            {
                var bankAccountsViewModel = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
                return View(bankAccountsViewModel);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccountsViewModel bankAccountsViewModel)
        {
            if (ModelState.IsValid && !BankAccountsViewModelExists(bankAccountsViewModel.Name))
            {
                bankAccountsViewModel.Id = Guid.NewGuid();
                _context.Add(bankAccountsViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccountsViewModel);
        }

        private bool BankAccountsViewModelExists(Guid id)
        {
            return _context.BankAccounts.Any(e => e.Id == id);
        }

        private bool BankAccountsViewModelExists(string name)
        {
            return _context.BankAccounts.Any(e => e.Name.ToUpper() == name.ToUpper());
        }
    }
}
