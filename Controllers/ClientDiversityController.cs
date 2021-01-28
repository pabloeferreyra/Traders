using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Traders.Data;
using Traders.Models;
using Traders.Services;

namespace Traders.Controllers
{
    [Authorize(Roles = "Trader")]
    public class ClientDiversityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IClientServices _clientServices;
        public ClientDiversityController(ApplicationDbContext context,
            IClientServices clientServices)
        {
            _context = context;
            _clientServices = clientServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clientServices.GetAllClientsDiversity());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientDiversityViewModel clientDiversityViewModel)
        {
            if (ModelState.IsValid)
            {
                bool created = await _clientServices.CreateClientDiversity(clientDiversityViewModel);
                if(created)
                    return RedirectToAction(nameof(Index));
                else
                    return View(clientDiversityViewModel);

            }
            return View(clientDiversityViewModel);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_clientServices.ClientDiversityViewModelExists((Guid)id))
            {
                var clientDiversityViewModel = _clientServices.GetClientDiversityById(id);
                return View(clientDiversityViewModel);
            }
            else
            {
                return NotFound();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClientDiversityViewModel clientDiversityViewModel)
        {
            if (!_clientServices.ClientDiversityViewModelExists(clientDiversityViewModel.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updated = await _clientServices.UpdateClientDiversity(clientDiversityViewModel);
                if(updated)
                    return RedirectToAction(nameof(Index));
                else
                    return View(clientDiversityViewModel);
            }
            return View(clientDiversityViewModel);
        }

        
    }
}
