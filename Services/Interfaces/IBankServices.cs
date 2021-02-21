using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traders.Models;

namespace Traders.Services
{
    public interface IBankServices 
    {
        public Task<List<BankAccountsViewModel>> GetBankAccounts();
        public bool BankAccountsViewModelExists(Guid id);
        public bool BankAccountsNameViewModelExists(string name);
        public Task<BankAccountsViewModel> GetBank(Guid? id);
        public Task<bool> CreateBank(BankAccountsViewModel model);
        public SelectList BankList();
        public Task<int> UpdateAmount(BankAccountsViewModel modelIn, BankAccountsViewModel modelOut);
        public Task<bool> EditAmount(BankAccountsViewModel model);
        public Task<bool> AddFutureAmount(decimal amount);
        public Task<bool> RetireFutureAmount(decimal amount);
    }
}
