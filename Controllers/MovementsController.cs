using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Traders.Data;
using Traders.Models;

namespace Traders.Controllers
{
    [Authorize(Roles = "Trader, Admin")]
    public class MovementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public MovementsController(ApplicationDbContext context,
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var movements = await _context.Movements.ToListAsync();
            List<MovementsViewModel> mov = new List<MovementsViewModel>();
            foreach (var m in movements)
            {
                m.BankAccounts = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == m.BankAccountGuidIn);
                m.BankAccountsS = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == m.BankAccountGuidOut);
                mov.Add(m);
            }
            return View(mov);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (MovementsViewModelExists((Guid)id))
            {
                var movementsViewModel = await _context.Movements.FirstOrDefaultAsync(m => m.Id == id);
                movementsViewModel.BankAccounts = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == movementsViewModel.BankAccountGuidIn);
                movementsViewModel.BankAccountsS = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == movementsViewModel.BankAccountGuidOut);

                return View(movementsViewModel);
            }

            return NotFound();
        }


        public IActionResult Create()
        {
            ClaimsPrincipal currentUser = this.User;
            ViewData["CurrentUser"] = currentUser.FindFirst(ClaimTypes.NameIdentifier).Subject.Name;
            ViewData["BankAccounts"] = new SelectList(_context.BankAccounts, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovementsViewModel movementsViewModel)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                Guid movementId = Guid.NewGuid();
                var movements = new MovementsViewModel
                {
                    Id = movementId,
                    UserGuid = currentUser.FindFirst(ClaimTypes.Name).Value,
                    AmountIn = movementsViewModel.AmountIn,
                    AmountOut = movementsViewModel.AmountOut,
                    BankAccountGuidIn = movementsViewModel.BankAccountGuidIn,
                    BankAccountGuidOut = movementsViewModel.BankAccountGuidOut,
                    CorrelationId = null
                };
                if (movementsViewModel.AmountInS > 0)
                    movements.Comission = (movementsViewModel.Comission / 2);
                else
                    movements.Comission = (movementsViewModel.Comission);
                _context.Add(movements);
                await _context.SaveChangesAsync();
                var accountIn = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidIn).FirstOrDefaultAsync();
                var accountOut = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidOut).FirstOrDefaultAsync();
                movements.BadgeIn = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidIn).Select(b => b.Currency).FirstOrDefaultAsync();
                movements.BadgeOut = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidOut).Select(b => b.Currency).FirstOrDefaultAsync();
                accountIn.Amount += movements.AmountIn;
                accountOut.Amount -= movements.AmountOut;
                _context.Update(accountIn);
                _context.Update(accountOut);
                await _context.SaveChangesAsync();
                if (movementsViewModel.AmountInS > 0)
                {
                    var movementsS = new MovementsViewModel
                    {
                        Id = Guid.NewGuid(),
                        UserGuid = currentUser.FindFirst(ClaimTypes.Name).Value,
                        AmountIn = movementsViewModel.AmountInS,
                        AmountOut = movementsViewModel.AmountOutS,
                        BadgeIn = movementsViewModel.BadgeInS,
                        BadgeOut = movementsViewModel.BadgeOutS,
                        BankAccountGuidIn = movementsViewModel.BankAccountGuidInS,
                        BankAccountGuidOut = movementsViewModel.BankAccountGuidOutS,
                        CorrelationId = movementId,
                        Comission = (movementsViewModel.Comission / 2)
                    };
                    _context.Add(movementsViewModel);
                    await _context.SaveChangesAsync();
                    var accountInS = await _context.BankAccounts.Where(b => b.Id == movementsS.BankAccountGuidIn).FirstOrDefaultAsync();
                    var accountOutS = await _context.BankAccounts.Where(b => b.Id == movementsS.BankAccountGuidOut).FirstOrDefaultAsync();
                    accountIn.Amount += movementsS.AmountIn;
                    accountOut.Amount -= movementsS.AmountOut;
                    _context.Update(accountInS);
                    _context.Update(accountOutS);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movementsViewModel);
        }

        private bool MovementsViewModelExists(Guid id)
        {
            return _context.Movements.Any(e => e.Id == id);
        }
    }
}