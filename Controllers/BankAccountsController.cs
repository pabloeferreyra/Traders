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
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankAccounts
        [Authorize(Roles = "Trader, Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankAccounts.ToListAsync());
        }

        // GET: BankAccounts/Details/5
        [Authorize(Roles = "Trader, Admin")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountsViewModel = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccountsViewModel == null)
            {
                return NotFound();
            }

            return View(bankAccountsViewModel);
        }

        // GET: BankAccounts/Create
        [Authorize(Roles = "Admin, Traders")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Traders")]
        public async Task<IActionResult> Create([Bind("Id,Name,Amount")] BankAccountsViewModel bankAccountsViewModel)
        {
            if (ModelState.IsValid)
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
    }
}
