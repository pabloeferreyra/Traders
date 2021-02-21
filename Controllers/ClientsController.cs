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
    [Authorize(Roles = "Trader, Admin")]
    public class ClientsController : Controller
    {
        private readonly IClientServices _clientService;

        public ClientsController(IClientServices clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clientService.GetAllClients());
        }

        public async Task<IActionResult> Details(Guid? Id)
        {
            if (Id == null || !_clientService.ClientExists((Guid)Id))
            {
                return NotFound();
            }

            return View(await _clientService.GetClientDetails((Guid)Id));
        }
    }
}
