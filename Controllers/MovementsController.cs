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
                m.Badges = await _context.Badges.FirstOrDefaultAsync(ba => ba.Id == m.BadgeGuidIn);
                m.BadgesS = await _context.Badges.FirstOrDefaultAsync(ba => ba.Id == m.BadgeGuidOut);
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
                movementsViewModel.Badges = await _context.Badges.FirstOrDefaultAsync(ba => ba.Id == movementsViewModel.BadgeGuidIn);
                movementsViewModel.BadgesS = await _context.Badges.FirstOrDefaultAsync(ba => ba.Id == movementsViewModel.BadgeGuidOut);
                movementsViewModel.BankAccounts = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == movementsViewModel.BankAccountGuidIn);
                movementsViewModel.BankAccountsS = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == movementsViewModel.BankAccountGuidOut);

                return View(movementsViewModel);
            }

            return NotFound();
        }


        public async Task<IActionResult> Create()
        {
            ClaimsPrincipal currentUser = this.User;
            ViewData["CurrentUser"] = currentUser.FindFirst(ClaimTypes.NameIdentifier).Subject.Name;
            ViewData["Badges"] = new SelectList(_context.Badges, "Id", "Name");
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
                var movements = new MovementsViewModel
                {
                    Id = Guid.NewGuid(),
                    UserGuid = currentUser.FindFirst(ClaimTypes.Name).Value,
                    AmountIn = movementsViewModel.AmountIn,
                    AmountOut = movementsViewModel.AmountOut,
                    BadgeGuidIn = movementsViewModel.BadgeGuidIn,
                    BadgeGuidOut = movementsViewModel.BadgeGuidOut,
                    BankAccountGuidIn = movementsViewModel.BankAccountGuidIn,
                    BankAccountGuidOut = movementsViewModel.BankAccountGuidOut
                };
                if (movementsViewModel.AmountInS > 0)
                    movements.Comission = (movementsViewModel.Comission / 2);
                else
                    movements.Comission = (movementsViewModel.Comission);
                _context.Add(movements);
                await _context.SaveChangesAsync();
                var accountIn = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidIn).FirstOrDefaultAsync();
                var accountOut = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidOut).FirstOrDefaultAsync();
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
                        AmountInS = movementsViewModel.AmountInS,
                        AmountOutS = movementsViewModel.AmountOutS,
                        BadgeGuidInS = movementsViewModel.BadgeGuidInS,
                        BadgeGuidOutS = movementsViewModel.BadgeGuidOutS,
                        BankAccountGuidInS = movementsViewModel.BankAccountGuidInS,
                        BankAccountGuidOutS = movementsViewModel.BankAccountGuidOutS,
                        Comission = (movementsViewModel.Comission / 2)
                    };
                    _context.Add(movementsViewModel);
                    await _context.SaveChangesAsync();
                    var accountInS = await _context.BankAccounts.Where(b => b.Id == movementsS.BankAccountGuidInS).FirstOrDefaultAsync();
                    var accountOutS = await _context.BankAccounts.Where(b => b.Id == movementsS.BankAccountGuidOutS).FirstOrDefaultAsync();
                    accountIn.Amount += movements.AmountInS;
                    accountOut.Amount -= movements.AmountOutS;
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