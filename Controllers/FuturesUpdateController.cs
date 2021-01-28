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
    public class FuturesUpdateController : Controller
    {
        private readonly IFuturesServices _futuresServices;

        public FuturesUpdateController(
            IFuturesServices futuresServices)
        {
            _futuresServices = futuresServices;
        }

        // GET: FuturesUpdate
        public async Task<IActionResult> Index()
        {
            return View(await _futuresServices.GetFuturesUpdates(null));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuturesUpdateViewModel futuresUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                await _futuresServices.CreateFutureUpdate(futuresUpdateViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(futuresUpdateViewModel);
        }
    }
}
