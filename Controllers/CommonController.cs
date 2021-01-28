﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traders.Data;

namespace Traders.Controllers
{
    [AllowAnonymous]
    public class CommonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CommonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public bool ClientExist([FromBody]int Code)
        {
            return _context.Clients.Where(c => c.Code == Code).Any();
        }
    }

    public class ClientExistBody
    {
        public int Code { get; set; }
    }
}
