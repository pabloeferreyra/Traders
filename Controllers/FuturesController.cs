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
        private readonly IBankServices _bankServices;
        private readonly IFuturesRetireServices _futuresRetireServices;

        public FuturesController(IClientServices clientServices,
            IFuturesServices futuresServices,
            IBankServices bankServices,
            IFuturesRetireServices futuresRetireServices)
        {
            _clientServices = clientServices;
            _futuresServices = futuresServices;
            _bankServices = bankServices;
            _futuresRetireServices = futuresRetireServices;
        }

        public async Task<IActionResult> Index()
        {
            var fixedRentContracts = await _futuresServices.GetContracts(true);
            List<FuturesViewModel> futuresUpdate = new List<FuturesViewModel>();
            foreach (var f in fixedRentContracts)
            {
                decimal finalOriginal = f.FinalResult;
                f.FinalResult = _futuresServices.FixRentCalc(f.Capital, f.FixRentPercentage, f.StartDate);
                f.LastGain =  f.FinalResult - f.Capital;

                if(finalOriginal != f.FinalResult)
                {
                    futuresUpdate.Add(f);
                }
            }
            await _futuresServices.UpdateFinalResultFixed(futuresUpdate);
            await _futuresServices.UpdateExpiredFutures();

            return View(await _futuresServices.GetFutures(null));
        }

        [HttpPost]
        public async Task<IActionResult> SearchFutures(DateTime? dateContract)
        {
            return PartialView("_FuturesPartial", await _futuresServices.GetFutures(dateContract));
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
                return View(futuresViewModel);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            int clientCode = _clientServices.ClientsCode();
            if (clientCode > 100)
            {
                clientCode += 1;
            }
            ViewData["ClientCode"] = clientCode;
            ViewData["ParticipationId"] = _futuresServices.Participations();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuturesViewModel futuresViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = await _clientServices.GetClient(futuresViewModel.Code);
                if (futuresViewModel.RefeerCode.HasValue) {
                   var refeer = await _clientServices.GetClient(futuresViewModel.RefeerCode.Value);
                    futuresViewModel.Refeer = refeer.Id;
                }
                if (client == null)
                {
                    client = new ClientsViewModel
                    {
                        Id = Guid.NewGuid(),
                        Code = futuresViewModel.Code,
                        Email = futuresViewModel.Email
                    };
                    await _clientServices.CreateClient(client);
                }
                else if(client.Email != futuresViewModel.Email)
                {
                    client.Email = futuresViewModel.Email;
                    await _clientServices.UpdateClient(client);
                }

                if (futuresViewModel.FixRent)
                    futuresViewModel.ParticipationId = null;
                var contract = await _futuresServices.GetLastContractNumber();
                futuresViewModel.Id = Guid.NewGuid();
                futuresViewModel.ContractNumber = contract + 1;
                futuresViewModel.ClientId = client.Id;
                if (!NoLimitclient(client.Code))
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddMonths(6);
                }
                else
                {
                    futuresViewModel.FinishDate = futuresViewModel.StartDate.AddYears(99);
                }
                
                await _futuresServices.CreateFuture(futuresViewModel);
                await _bankServices.AddFutureAmount(futuresViewModel.Capital);
                return RedirectToAction(nameof(Index));
            }
            return View(futuresViewModel);
        }

        private static bool NoLimitclient(int clientCode)
        {

            return clientCode switch
            {
                (int)ClientsTypes.SpecialClients.Uno or (int)ClientsTypes.SpecialClients.Dos or (int)ClientsTypes.SpecialClients.Tres or (int)ClientsTypes.SpecialClients.Cuatro or (int)ClientsTypes.SpecialClients.Cinco => true,
                _ => false,
            };
        }
    }
}
