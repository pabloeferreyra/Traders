using Microsoft.EntityFrameworkCore;
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
        
        public ClientServices(ApplicationDbContext context)
        {
            _context = context;
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
            return await _context.clientDiversities.ToListAsync();
        }

        public async Task<ClientDiversityViewModel> GetClientDiversityById(Guid? id)
        {
            return await _context.clientDiversities.FindAsync(id);
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
            return _context.clientDiversities.Any(e => e.Id == id);
        }

        public int ClientsCode()
        {
            var clients = _context.Clients.OrderBy(c => c.Code);
            int clientCode = 100;
            if (clients.Count() > 0)
            {
                clientCode = clients.LastOrDefault().Code;
            }
            return clientCode;
        }

        public async Task<ClientsViewModel> CreateClient(ClientsViewModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
