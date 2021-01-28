using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Traders.Models;
using Traders.Services;
using Traders.Settings;
using static Traders.Settings.ClientsTypes;

namespace Traders.Controllers
{
    [Authorize(Roles = "Trader")]
    public class FuturesController : Controller
    {
        private readonly IClientServices _clientServices;
        private readonly IFuturesServices _futuresServices;

        public FuturesController(IClientServices clientServices,
            IFuturesServices futuresServices)
        {
            _clientServices = clientServices;
            _futuresServices = futuresServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _futuresServices.GetFutures());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_futuresServices.FuturesViewModelExists((Guid)id))
            {
                FuturesViewModel futuresViewModel = await _futuresServices.GetFuture(id);
                var contracts = await _futuresServices.CountContracts(false);
                if (!futuresViewModel.FixRent)
                {
                    
                    futuresViewModel.FuturesUpdates = new List<FuturesUpdateViewModel>();
                    List<FuturesUpdateViewModel> futuresUpdates = await _futuresServices.GetFuturesUpdates(futuresViewModel.StartDate);
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

                        futuresViewModel.FinalResult = futuresViewModel.FinalResult + fuGain;

                        if(futuresViewModel.Client.Code == (int)SpecialClients.Uno)
                        {
                            var futuresWithFixed = await _futuresServices.GetContracts(true);
                            futuresViewModel.FinalResult = _futuresServices.FinalResult(futuresWithFixed, futuresViewModel.FinalResult);
                        }
                    }
                    else
                    {
                        futuresViewModel.FinalResult = futuresViewModel.Capital;
                    }
                }
                else
                {
                    futuresViewModel.FinalResult = _futuresServices.FixRentCalc(futuresViewModel.Capital);
                }
                return View(futuresViewModel);
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Renewal(Guid? id)
        {
            if (id != null || _futuresServices.FuturesViewModelExists((Guid)id))
            {
                FuturesViewModel futuresViewModel = await _futuresServices.GetFuture(id);
                var contract = await _futuresServices.GetLastContractNumber();
                futuresViewModel.Id = Guid.NewGuid();
                futuresViewModel.ContractNumber = contract++;
                futuresViewModel.StartDate = DateTime.Today;
                if (!NoLimitclient(futuresViewModel.Client.Code))
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddMonths(6);
                }
                else
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddYears(99);
                }

                List<FuturesUpdateViewModel> futuresUpdates = await _futuresServices.GetFuturesUpdates(futuresViewModel.StartDate);
                if (futuresUpdates.Count > 0)
                {
                    if (!futuresViewModel.FixRent)
                    {
                        decimal fuGain = 0;
                        var contracts = await _futuresServices.CountContracts(false);
                        foreach (var fu in futuresUpdates)
                        {
                            var gain = fu.Gain / contracts;
                            fuGain += ((futuresViewModel.Capital + gain) / (futuresViewModel.Participation.Percentage / 100));
                        }

                        futuresViewModel.Capital += fuGain;
                    }
                    else
                    {
                        futuresViewModel.Capital = _futuresServices.FixRentCalc(futuresViewModel.Capital);
                    }
                }
                else
                {
                    futuresViewModel.Capital = futuresViewModel.Capital;
                }
                await _futuresServices.CreateFuture(futuresViewModel);
                return RedirectToAction("Details", "Futures", new { @id = futuresViewModel.Id });
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            int clientCode = _clientServices.ClientsCode();
            ViewData["ClientCode"] = clientCode++;
            ViewData["ParticipationId"] = _futuresServices.Participations();
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
                    Id = Guid.NewGuid(),
                    Code = futuresViewModel.Code,
                    Email = futuresViewModel.Email
                };
                if (futuresViewModel.FixRent)
                    futuresViewModel.ParticipationId = null;
                var contract = await _futuresServices.GetLastContractNumber();
                futuresViewModel.Id = Guid.NewGuid();
                futuresViewModel.ContractNumber = contract + 1;
                futuresViewModel.ClientId = client.Id;
                if (!NoLimitclient(futuresViewModel.Client.Code))
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddMonths(6);
                }
                else
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddYears(99);
                }
                await _clientServices.CreateClient(client);
                await _futuresServices.CreateFuture(futuresViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(futuresViewModel);
        }

        private bool NoLimitclient(int clientCode)
        {
            
            switch (clientCode)
            {
                case (int)ClientsTypes.SpecialClients.Uno:
                case (int)ClientsTypes.SpecialClients.Dos:
                case (int)ClientsTypes.SpecialClients.Tres:
                case (int)ClientsTypes.SpecialClients.Cuatro:
                case (int)ClientsTypes.SpecialClients.Cinco:
                    return true;
                default:
                    return false;
            }
            
        }
    }
}
