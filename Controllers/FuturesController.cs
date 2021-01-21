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
    public class FuturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Futures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Futures.Include(f => f.Client).Include(f => f.Participation);
            var futures = await applicationDbContext.ToListAsync();
            for (int f = 0; f < futures.Count(); f++)
            {
                if (!NoLimitclient(futures[f].Client.Code))
                {
                    futures[f].FinishDate = futures[f].StartDate.AddMonths(6);
                }
                else
                {
                    futures[f].FinishDate = futures[f].StartDate.AddYears(99);
                }
            }
            return View(futures);
        }

        // GET: Futures/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (FuturesViewModelExists((Guid)id))
            {
                FuturesViewModel futuresViewModel = await _context.Futures
                .Include(f => f.Client)
                .Include(f => f.Participation)
                .FirstOrDefaultAsync(m => m.Id == id);
                var contracts = await _context.Futures.Where(f => f.StartDate.AddMonths(6) < DateTime.Now).CountAsync();
                if (!NoLimitclient(futuresViewModel.Client.Code))
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddMonths(6);
                }
                else
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddYears(99);
                }
                futuresViewModel.FuturesUpdates = new List<FuturesUpdateViewModel>();
                List<FuturesUpdateViewModel> futuresUpdates = await _context.FuturesUpdates.Where(fu => fu.ModifDate >= futuresViewModel.StartDate).OrderBy(fu => fu.ModifDate).ToListAsync();
                if (futuresUpdates.Count > 0)
                {
                    foreach (var fu in futuresUpdates)
                    {
                        var gain = fu.Gain / contracts;
                        fu.GainFinal = ((futuresViewModel.Capital + gain) / (futuresViewModel.Participation.Percentage / 100));
                        futuresViewModel.FuturesUpdates.Add(fu);
                    }

                    decimal fuGain = 0;

                    foreach (var fu in futuresUpdates)
                    {
                        var gain = fu.Gain / contracts;
                        fuGain += ((futuresViewModel.Capital + gain) / (futuresViewModel.Participation.Percentage / 100));
                    }

                    futuresViewModel.FinalResult += fuGain;
                }
                else
                {
                    futuresViewModel.FinalResult = futuresViewModel.Capital;
                }
                return View(futuresViewModel);
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Renewal(Guid? id)
        {
            if (id != null || FuturesViewModelExists((Guid)id))
            {
                FuturesViewModel futuresViewModel = await _context.Futures
                .Include(f => f.Client)
                .Include(f => f.Participation)
                .FirstOrDefaultAsync(m => m.Id == id);
                var contract = await _context.Futures.OrderBy(c => c.ContractNumber).Select(c => c.ContractNumber).LastOrDefaultAsync();
                futuresViewModel.Id = Guid.NewGuid();
                futuresViewModel.ContractNumber = contract++;
                futuresViewModel.StartDate = DateTime.Today;
                futuresViewModel.FinishDate = futuresViewModel.StartDate.AddMonths(6);

                List<FuturesUpdateViewModel> futuresUpdates = await _context.FuturesUpdates.Where(fu => fu.ModifDate >= futuresViewModel.StartDate).OrderBy(fu => fu.ModifDate).ToListAsync();
                if (futuresUpdates.Count > 0)
                {
                    decimal fuGain = 0;

                    foreach (var fu in futuresUpdates)
                    {
                        fuGain += ((futuresViewModel.Capital * (fu.Gain / 100)) / (futuresViewModel.Participation.Percentage / 100));
                    }

                    futuresViewModel.Capital += fuGain;
                }
                else
                {
                    futuresViewModel.Capital = futuresViewModel.Capital;
                }
                _context.Futures.Add(futuresViewModel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Futures", new { @id = futuresViewModel.Id });
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            var clients = _context.Clients.OrderBy(c => c.Code);
            int clientCode = 100;
            if (clients.Count() > 0)
            {
                clientCode = clients.LastOrDefault().Code;
            }
            ViewData["ClientCode"] = clientCode++;
            ViewData["ParticipationId"] = new SelectList(_context.Participations, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuturesViewModel futuresViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = new ClientsViewModel
                {
                    Code = futuresViewModel.Code,
                    Email = futuresViewModel.Email
                };
                var contract = await _context.Futures.OrderBy(c => c.ContractNumber).Select(c => c.ContractNumber).LastOrDefaultAsync();
                futuresViewModel.Id = Guid.NewGuid();
                futuresViewModel.ContractNumber = contract++;
                _context.Add(client);
                _context.Add(futuresViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParticipationId"] = new SelectList(_context.Participations, "Id", "Name", futuresViewModel.ParticipationId);
            return View(futuresViewModel);
        }

        private bool FuturesViewModelExists(Guid id)
        {
            return _context.Futures.Any(e => e.Id == id);
        }

        private bool NoLimitclient(int clientCode)
        {
            var noLimitClient = new int[] { 001, 002, 003, 004, 005 };
            if(noLimitClient.Contains(clientCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
