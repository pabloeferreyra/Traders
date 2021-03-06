﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traders.Data;
using Traders.Models;

namespace Traders.Services
{
    public class ClientServices : IClientServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IFuturesServices _futuresServices;
        
        public ClientServices(ApplicationDbContext context,
            IFuturesServices futuresServices)
        {
            _context = context;
            _futuresServices = futuresServices;
        }

        public async Task<bool> CreateClientDiversity(ClientDiversityViewModel model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                _context.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ClientDiversityViewModel>> GetAllClientsDiversity()
        {
            return await _context.ClientDiversities.ToListAsync();
        }

        public async Task<List<ClientsViewModel>> GetAllClients()
        {
            var clients = await _context.Clients.ToListAsync();
            for(int i = 0; i < clients.Count; i++)
            {
                clients[i].Contracts = (await _futuresServices.GetFuturesForClient((Guid)clients[i].Id)).Count;
            }
            return clients;
        }

        public async Task<ClientDiversityViewModel> GetClientDiversityById(Guid? id)
        {
            return await _context.ClientDiversities.FindAsync(id);
        }

        public async Task<bool> UpdateClientDiversity(ClientDiversityViewModel model)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool ClientDiversityViewModelExists(Guid id)
        {
            return _context.ClientDiversities.Any(e => e.Id == id);
        }

        public bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        public async Task<ClientsViewModel> CreateClient(ClientsViewModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ClientsViewModel> GetClientDetails(Guid id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            client.Futures = await _futuresServices.GetFuturesForClient(id);
            return client;
        }

        public async Task<ClientsViewModel> UpdateClient(ClientsViewModel model)
        {
            _context.Clients.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<ClientsViewModel> GetClient(string dni)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Dni == dni);
        }

        public bool ClientExistByDni(string dni)
        {
            return _context.Clients.Where(c => c.Dni == dni).Any();
        }
    }
}
