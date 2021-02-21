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
using Traders.Services;

namespace Traders.Controllers
{
    [Authorize(Roles = "Trader")]
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBankServices _bankServices;
        public BankAccountsController(ApplicationDbContext context,
            IBankServices bankServices)
        {
            _context = context;
            _bankServices = bankServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _bankServices.GetBankAccounts());
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_bankServices.BankAccountsViewModelExists((Guid)id))
            {
                return View(await _bankServices.GetBank(id));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BankAccountsViewModel bankAccountsViewModel)
        {
            if (ModelState.IsValid && _bankServices.BankAccountsNameViewModelExists(bankAccountsViewModel.Currency))
            {
                
                await _bankServices.EditAmount(bankAccountsViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccountsViewModel);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_bankServices.BankAccountsViewModelExists((Guid)id))
            {
                return View(await _bankServices.GetBank(id));
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
            if (ModelState.IsValid && !_bankServices.BankAccountsNameViewModelExists(bankAccountsViewModel.Currency))
            {
                await _bankServices.CreateBank(bankAccountsViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccountsViewModel);
        }

    }
}
