using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traders.Data;
using Traders.Models;

namespace Traders.Services
{
    public class BankServices : IBankServices
    {
        private readonly ApplicationDbContext _context;

        public BankServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool BankAccountsViewModelExists(Guid id)
        {
            return _context.BankAccounts.Any(e => e.Id == id);
        }

        public bool BankAccountsNameViewModelExists(string name)
        {
            return _context.BankAccounts.Any(e => e.Currency.ToUpper() == name.ToUpper());
        }

        public async Task<List<BankAccountsViewModel>> GetBankAccounts()
        {
            return await _context.BankAccounts.ToListAsync();
        }

        public async Task<BankAccountsViewModel> GetBank(Guid? id)
        {
            return await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> CreateBank(BankAccountsViewModel model)
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

        public SelectList BankList()
        {
            return new SelectList(_context.BankAccounts,
                                                      "Id",
                                                      "Currency");
        }

        public async Task<bool> EditAmount(BankAccountsViewModel model)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddFutureAmount(decimal amount)
        {
            try
            {
                var usd = await _context.BankAccounts.Where(b => b.Currency == "USDT Futuros").FirstOrDefaultAsync();
                usd.Amount = usd.Amount + amount;
                _context.Update(usd);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RetireFutureAmount(decimal amount)
        {
            try
            {
                var usd = await _context.BankAccounts.Where(b => b.Currency == "USDT Futuros").FirstOrDefaultAsync();
                BankAccountsViewModel model = new BankAccountsViewModel
                {
                    Id = usd.Id,
                    Currency = usd.Currency,
                    Amount = usd.Amount - amount
                };
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> UpdateAmount(BankAccountsViewModel modelIn,
                                             BankAccountsViewModel modelOut)
        {
            _context.Update(modelIn);
            _context.Update(modelOut);
            return await _context.SaveChangesAsync();
        }
    }
}
