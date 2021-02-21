using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Traders.Data;
using Traders.Models;
using Traders.Services;

namespace Traders.Controllers
{
    public class RetireFuturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFuturesServices _futuresServices;
        private readonly IFuturesRetireServices _futuresRetireServices;

        public RetireFuturesController(ApplicationDbContext context,
            IFuturesServices futuresServices,
            IFuturesRetireServices futuresRetireServices)
        {
            _context = context;
            _futuresServices = futuresServices;
            _futuresRetireServices = futuresRetireServices;
        }

        public async Task<IActionResult> Index(int contractNumber)
        {
            if (await _futuresServices.FuturesNumberExists(contractNumber))
            {
                return View(await _futuresRetireServices.GetAllRetires(contractNumber));
            }
            return NotFound();
        }

        public async Task<IActionResult> Create(Guid? id)
        {
            if (id != null && _futuresServices.FuturesViewModelExists((Guid)id))
            {
                var futures = await _futuresServices.GetFuture((Guid)id);
                RetireFuturesViewModel model = new RetireFuturesViewModel
                {
                    ContractNumber = futures.ContractNumber,
                    Capital = await _futuresServices.GetResult(futures)
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RetireFuturesViewModel retireFuturesViewModel)
        {
            if (ModelState.IsValid)
            {
                await this._futuresRetireServices.Create(retireFuturesViewModel);
                return RedirectToAction("Index", "Futures");
            }
            return View(retireFuturesViewModel);
        }        
    }
}
