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
using Traders.Services;

namespace Traders.Controllers
{
    [Authorize(Roles = "Trader, Admin")]
    public class MovementsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMovementsServices _movementsServices;
        private readonly IBankServices _bankServices;

        public MovementsController(
                                UserManager<IdentityUser> userManager,
                                IMovementsServices movementsServices,
                                IBankServices bankServices)
        {
            this.userManager = userManager;
            _movementsServices = movementsServices;
            _bankServices = bankServices;
        }

        public async Task<IActionResult> Index()
        {
            var movements = await _movementsServices.GetAllMovements();
            List<MovementsViewModel> mov = new List<MovementsViewModel>();
            foreach (var m in movements)
            {
                m.BankAccounts = await _bankServices.GetBank(m.BankAccountGuidIn);
                m.BankAccountsS = await _bankServices.GetBank(m.BankAccountGuidOut);
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

            if (_movementsServices.MovementsViewModelExists((Guid)id))
            {
                var movementsViewModel = await _movementsServices.GetMovements(id);
                movementsViewModel.BankAccounts = await _bankServices.GetBank(movementsViewModel.BankAccountGuidIn);
                movementsViewModel.BankAccountsS = await _bankServices.GetBank(movementsViewModel.BankAccountGuidOut);

                return View(movementsViewModel);
            }

            return NotFound();
        }


        public IActionResult Create()
        {
            ClaimsPrincipal currentUser = this.User;
            ViewData["CurrentUser"] = currentUser.FindFirst(ClaimTypes.NameIdentifier).Subject.Name;
            ViewData["BankAccounts"] = _bankServices.BankList();
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
                await _movementsServices.CreateMovement(movementsViewModel);
                var accountIn = await _bankServices.GetBank(movements.BankAccountGuidIn);
                var accountOut = await _bankServices.GetBank(movements.BankAccountGuidOut);
                movements.BadgeIn = accountIn.Currency;
                movements.BadgeOut = accountOut.Currency;
                accountIn.Amount += movements.AmountIn;
                accountOut.Amount -= movements.AmountOut;
                await _bankServices.UpdateAmmount(accountIn, accountOut);
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
                    await _movementsServices.CreateMovement(movementsViewModel);
                    var accountInS = await _bankServices.GetBank(movementsS.BankAccountGuidIn);
                    var accountOutS = await _bankServices.GetBank(movementsS.BankAccountGuidOut);
                    accountIn.Amount += movementsS.AmountIn;
                    accountOut.Amount -= movementsS.AmountOut;
                    await _bankServices.UpdateAmmount(accountInS, accountOutS);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movementsViewModel);
        }
    }
}