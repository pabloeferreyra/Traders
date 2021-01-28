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
    public class FuturesServices : IFuturesServices
    {
        private readonly ApplicationDbContext _context;
        public FuturesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FuturesViewModel>> GetFutures()
        {
            var futures = await _context.Futures
                .Include(f => f.Client)
                .Include(f => f.Participation).ToListAsync();
            return futures;
        }

        public bool FuturesViewModelExists(Guid id)
        {
            return _context.Futures.Any(e => e.Id == id);
        }

        public async Task<FuturesViewModel> GetFuture(Guid? id)
        {
            return await _context.Futures
                .Include(f => f.Client)
                .Include(f => f.Participation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetLastContractNumber()
        {
            return await _context.Futures.OrderBy(c => c.ContractNumber).Select(c => c.ContractNumber).LastOrDefaultAsync();
        }

        public async Task<List<FuturesUpdateViewModel>> GetFuturesUpdates(DateTime? startDate)
        {
            if (startDate != null)
            {
                return await _context.FuturesUpdates.Where(fu => fu.ModifDate >= startDate).OrderBy(fu => fu.ModifDate).ToListAsync();
            }
            else
            {
                return await _context.FuturesUpdates.ToListAsync();
            }
        }

        public async Task<bool> CreateFuture(FuturesViewModel model)
        {
            try
            {
                _context.Futures.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<int> CountContracts(bool fixRent)
        {
            return await _context.Futures.Where(f => f.StartDate <= DateTime.Now && f.FinishDate >= DateTime.Now && f.FixRent == fixRent).CountAsync();
        }

        public async Task<List<FuturesViewModel>> GetContracts(bool fixRent)
        {
            return await _context.Futures.Include(f => f.Client).Where(f => f.StartDate <= DateTime.Now && f.FinishDate >= DateTime.Now && f.FixRent == fixRent).ToListAsync();
        }
        public async Task<List<FuturesViewModel>> GetAllContracts()
        {
            return await _context.Futures.Include(f => f.Client).Where(f => f.StartDate <= DateTime.Now && f.FinishDate >= DateTime.Now).ToListAsync();
        }

        public SelectList Participations()
        {
            return new SelectList(_context.Participations, "Id", "Name");
        }

        public async Task<List<FuturesUpdateViewModel>> GetFuturesUpdatesForMail(DateTime startDate, DateTime finishDate)
        {
            return await _context.FuturesUpdates.Where(m => m.ModifDate >= startDate && m.ModifDate <= finishDate).OrderBy(m => m.ModifDate).ToListAsync();
        }

        public int GetParticipation(Guid? participationId)
        {
            return _context.Participations.Where(p => p.Id == participationId).Select(p => p.Percentage).FirstOrDefault();
        }

        public decimal FinalResult(List<FuturesViewModel> futuresWithFixed, decimal finalResult)
        {
            var final = finalResult;
            foreach (var ff in futuresWithFixed)
            {
                ff.FinalResult = (decimal)Math.Pow(1.05, 6) * ff.Capital;
                decimal gainFix = ff.FinalResult - ff.Capital;
                final = final - gainFix;
            }
            return final;
        }

        public decimal FixRentCalc(decimal capital)
        {
            return (decimal)Math.Pow(1.05, 6) * capital;
        }

        public async Task<int> CreateFutureUpdate(FuturesUpdateViewModel model)
        {
            model.Id = Guid.NewGuid();
            _context.Add(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> FuturesUpdateViewModelExists(Guid id)
        {
            return await _context.FuturesUpdates.AnyAsync(e => e.Id == id);
        }
    }
}
