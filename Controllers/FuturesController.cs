﻿using System;
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
                return View(futuresViewModel);
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
                var client = await _clientServices.GetClient(futuresViewModel.Code);
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
