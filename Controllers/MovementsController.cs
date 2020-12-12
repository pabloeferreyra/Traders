using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> userManager;

        public MovementsController(ApplicationDbContext context,
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Movements
        public async Task<IActionResult> Index()
        {
            var movements = await _context.Movements.ToListAsync();
            for (int i = 0; i <= movements.Count(); i++)
            {
                var movement = movements[i];
                movements[i].UserGuid = userManager.FindByIdAsync(movement.UserGuid).Result.UserName;
            }
            return View(movements);
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
            else
            {
                movementsViewModel.UserGuid = userManager.FindByIdAsync(movementsViewModel.UserGuid).Result.UserName;
            }

            return View(movementsViewModel);
        }

        // GET: Movements/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateMov,UserGuid,AmountIn,BadgeGuidIn,BankAccountGuidIn,AmountOut,BadgeGuidOut,BankAccountGuidOut, Commission")] MovementsViewModel movementsViewModel)
        {
            if (ModelState.IsValid)
            {
                movementsViewModel.Id = Guid.NewGuid();
                movementsViewModel.UserGuid = User.FindFirstValue(ClaimTypes.Name);
                _context.Add(movementsViewModel);
                await _context.SaveChangesAsync();
                var accountIn = await _context.BankAccounts.Where(b => b.Id == movementsViewModel.BankAccountGuidIn).FirstOrDefaultAsync();
                var accountOut = await _context.BankAccounts.Where(b => b.Id == movementsViewModel.BankAccountGuidOut).FirstOrDefaultAsync();
                accountIn.Amount += movementsViewModel.AmountIn;
                accountOut.Amount += movementsViewModel.AmountOut;
                _context.Update(accountIn);
                _context.Update(accountOut);
                await _context.SaveChangesAsync();
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
