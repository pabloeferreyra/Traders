﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traders.Data;
using Traders.Models;
using Traders.Services;

namespace Traders.Controllers
{
    [AllowAnonymous]
    public class CommonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IClientServices _clientServices;
        public CommonController(ApplicationDbContext context,
            IClientServices clientServices)
        {
            _context = context;
            _clientServices = clientServices;
        }

        [HttpGet]
        public bool ClientExist(string email)
        {
            return _context.Clients.Where(c => c.Dni == email).Any();
        }

        [HttpGet]
        public async Task<JsonResult> GetClient(string dni)
        {
            var client = await _clientServices.GetClient(dni);
            return Json(client);
        }

        [HttpGet]
        public JsonResult CalculateTime(DateTime date)
        {
            return Json(date.AddMonths(6).ToShortDateString());
        }

        [HttpGet]
        public bool CurrencyExists(string currency)
        {
            return _context.BankAccounts.Where(c => c.Currency == currency.ToUpper()).Any();
        }
    }
}
