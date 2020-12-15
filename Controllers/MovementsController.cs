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
using Traders.Data;
using Traders.Models;

namespace Traders.Controllers
{
    [Authorize(Roles = "Trader")]
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
            return View(await _context.Movements.ToListAsync());
        }

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

        public async Task<IActionResult> Create()
        {
            List<BadgesViewModel> Badges = await _context.Badges.ToListAsync();
            ViewBag.BadgesIn = Badges;
            ViewBag.BadgesOut = Badges;
            return View();
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovementsViewModel movementsViewModel)
        {
            if(ModelState.IsValid)
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
                    BankAccountGuidOut = movementsViewModel.BankAccountGuidOut,
                    Comission = (movementsViewModel.Comission / 2)
                };
                _context.Add(movements);
                await _context.SaveChangesAsync();
                var accountIn = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidIn).FirstOrDefaultAsync();
                var accountOut = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidOut).FirstOrDefaultAsync();
                accountIn.Amount += movements.AmountIn;
                accountOut.Amount += movements.AmountOut;
                _context.Update(accountIn);
                _context.Update(accountOut);
                await _context.SaveChangesAsync();
                if(movementsViewModel.AmountInS > 0)
                {
                    var movementsS = new MovementsViewModel
                    {
                        Id = Guid.NewGuid(),
                        UserGuid = currentUser.FindFirst(ClaimTypes.Name).Value,
                        AmountIn = movementsViewModel.AmountInS,
                        AmountOut = movementsViewModel.AmountOutS,
                        BadgeGuidIn = movementsViewModel.BadgeGuidInS,
                        BadgeGuidOut = movementsViewModel.BadgeGuidOutS,
                        BankAccountGuidIn = movementsViewModel.BankAccountGuidInS,
                        BankAccountGuidOut = movementsViewModel.BankAccountGuidOutS,
                        Comission = (movementsViewModel.Comission / 2)
                    };
                    _context.Add(movementsViewModel);
                    await _context.SaveChangesAsync();
                    var accountInS = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidInS).FirstOrDefaultAsync();
                    var accountOutS = await _context.BankAccounts.Where(b => b.Id == movements.BankAccountGuidOutS).FirstOrDefaultAsync();
                    accountIn.Amount += movements.AmountInS;
                    accountOut.Amount += movements.AmountOutS;
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
