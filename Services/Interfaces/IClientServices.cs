using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traders.Models;

namespace Traders.Services
{
    public interface IClientServices
    {
        public Task<ClientDiversityViewModel> GetClientDiversityById(Guid? id);
        public Task<List<ClientDiversityViewModel>> GetAllClientsDiversity();
        public Task<bool> CreateClientDiversity(ClientDiversityViewModel model);
        public Task<bool> UpdateClientDiversity(ClientDiversityViewModel model);
        public bool ClientDiversityViewModelExists(Guid id);
        public Task<ClientsViewModel> CreateClient(ClientsViewModel model);
        public Task<ClientsViewModel> GetClient(string dni);
        public Task<ClientsViewModel> UpdateClient(ClientsViewModel model);
        public Task<ClientsViewModel> GetClientDetails(Guid id);
        public Task<List<ClientsViewModel>> GetAllClients();
        public bool ClientExists(Guid id);
        public bool ClientExistByDni(string dni);
    }
}
