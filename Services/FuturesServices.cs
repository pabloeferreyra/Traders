using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traders.Data;
using Traders.Models;
using Traders.Settings;
using static Traders.Settings.ClientsTypes;

namespace Traders.Services
{
    public class FuturesServices : IFuturesServices
    {
        private readonly ApplicationDbContext _context;
        public FuturesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FuturesViewModel>> GetFuturesForClient(Guid clientId)
        {
            var futures = await _context.Futures
                .Include(f => f.Client)
                .Include(f => f.Participation).Where(f => f.ClientId == clientId).ToListAsync();
            return await CalculateFutures(futures);
        }

        public async Task<List<FuturesViewModel>> CalculateFutures(List<FuturesViewModel> futures)
        {
            List<FuturesViewModel> futuresNew = new List<FuturesViewModel>();
            foreach (var f in futures)
            {
                var contracts = await CountContracts();
                if (!f.FixRent)
                {

                    f.FuturesUpdates = new List<FuturesUpdateViewModel>();
                    List<FuturesUpdateViewModel> futuresUpdates = await GetFuturesUpdates(f.StartDate);
                    if (futuresUpdates.Count > 0)
                    {
                        foreach (var fu in futuresUpdates)
                        {
                            var gain = fu.Gain / contracts;
                            fu.GainFinal = ((f.Capital + gain) / (f.Participation.Percentage / 100));
                            f.FuturesUpdates.Add(fu);
                        }

                        decimal fuGain = 0;

                        foreach (var fu in futuresUpdates)
                        {
                            var gain = fu.Gain / contracts;
                            fuGain += ((f.Capital + gain) / (f.Participation.Percentage / 100));
                        }

                        f.FinalResult += fuGain;

                        if (f.Client.Code == (int)SpecialClients.Uno)
                        {
                            var futuresWithFixed = await GetContracts(true);
                            f.FinalResult = FinalResult(futuresWithFixed, f.FinalResult);
                        }
                    }
                    else
                    {
                        f.FinalResult = f.Capital;
                    }
                }
                else
                {
                    f.FinalResult = FixRentCalc(f.Capital, f.FixRentPercentage, f.StartDate);
                }

                futuresNew.Add(f);
            }
            return futuresNew;
        }

        public async Task<List<FuturesViewModel>> GetFutures(DateTime? date)
        {
            List<FuturesViewModel> futures = new List<FuturesViewModel>();
            if (date == null)
            {
                futures = await _context.Futures
                    .Include(f => f.Client)
                    .Include(f => f.Participation).ToListAsync();
            }
            else
            {
                futures = await _context.Futures
                    .Include(f => f.Client)
                    .Include(f => f.Participation).Where(f => f.FinishDate == date).ToListAsync();
            }
            return await CalculateFutures(futures);
        }

        public bool FuturesViewModelExists(Guid id)
        {
            return _context.Futures.Any(e => e.Id == id);
        }

        public async Task<FuturesViewModel> GetFuture(Guid? id)
        {
            var future = await _context.Futures
                .Include(f => f.Client)
                .Include(f => f.Participation)
                .FirstOrDefaultAsync(m => m.Id == id);

            var contracts = await CountContracts();
            if (!future.FixRent)
            {

                future.FuturesUpdates = new List<FuturesUpdateViewModel>();
                List<FuturesUpdateViewModel> futuresUpdates = await GetFuturesUpdates(future.StartDate);
                if (futuresUpdates.Count > 0)
                {
                    foreach (var fu in futuresUpdates)
                    {
                        var gain = fu.Gain / contracts;
                        fu.GainFinal = ((future.Capital + gain) / (future.Participation.Percentage / 100));
                        future.FuturesUpdates.Add(fu);
                    }

                    decimal fuGain = 0;

                    foreach (var fu in futuresUpdates)
                    {
                        var gain = fu.Gain / contracts;
                        fuGain += ((future.Capital + gain) / (future.Participation.Percentage / 100));
                    }

                    future.FinalResult += fuGain;

                    if (future.Client.Code == (int)SpecialClients.Uno)
                    {
                        var futuresWithFixed = await GetContracts(true);
                        future.FinalResult = FinalResult(futuresWithFixed, future.FinalResult);
                    }
                }
                else
                {
                    future.FinalResult = future.Capital;
                }
            }
            else
            {
                future.FinalResult = FixRentCalc(future.Capital, future.FixRentPercentage, future.StartDate);
            }
            return future;
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

        public async Task<int> CountContracts()
        {
            return await _context.Futures.Where(f => f.StartDate <= DateTime.Now && f.FinishDate >= DateTime.Now).CountAsync();
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
                double rent = (double)((ff.FixRentPercentage / 100) + 1);
                ff.FinalResult = (decimal)Math.Pow(rent, 6) * ff.Capital;
                decimal gainFix = ff.FinalResult - ff.Capital;
                final -= gainFix;
            }
            return final;
        }

        public decimal FixRentCalc(decimal capital, decimal fixRentPercentage, DateTime startDate)
        {
            int months = Math.Abs(12 * (startDate.Year - DateTime.Now.Year) + startDate.Month - DateTime.Now.Month);
            double rentPercentage = (double)((fixRentPercentage / 100) + 1);
            if (months != 0)
                return (decimal)Math.Pow(rentPercentage, months) * capital;
            else
                return capital;
        }

        public async Task<int> CreateFutureUpdate(FuturesUpdateViewModel model)
        {
            model.Id = Guid.NewGuid();
            _context.Add(model);
            var futures = await GetContracts(false);
            var ret = await _context.SaveChangesAsync();
            foreach (var f in futures)
            {
                f.FinalResult = await GetResult(f);
                _context.Futures.Update(f);
                ret = await _context.SaveChangesAsync();
            }
            return ret;
        }

        public async Task<bool> FuturesUpdateViewModelExists(Guid id)
        {
            return await _context.FuturesUpdates.AnyAsync(e => e.Id == id);
        }

        public async Task<int> RetireFuture(FuturesViewModel model)
        {
            _context.Futures.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> FuturesNumberExists(int cNumber)
        {
            return await _context.Futures.AnyAsync(f => f.ContractNumber == cNumber);
        }

        public async Task<decimal> GetResult(FuturesViewModel futuresViewModel)
        {
            var contracts = await CountContracts();
            if (!futuresViewModel.FixRent)
            {
                futuresViewModel.FuturesUpdates = new List<FuturesUpdateViewModel>();
                List<FuturesUpdateViewModel> futuresUpdates = await GetFuturesUpdates(futuresViewModel.StartDate);
                if (futuresUpdates.Count > 0)
                {
                    foreach (var fu in futuresUpdates)
                    {
                        var gain = fu.Gain / contracts;
                        fu.GainFinal = ((futuresViewModel.Capital + gain) / (futuresViewModel.Participation.Percentage / 100));
                        futuresViewModel.FuturesUpdates.Add(fu);
                    }

                    decimal fuGain = 0;

                    foreach (var fu in futuresUpdates)
                    {
                        var gain = fu.Gain / contracts;
                        fuGain += ((futuresViewModel.Capital + gain) / (futuresViewModel.Participation.Percentage / 100));
                    }

                    futuresViewModel.FinalResult += fuGain;

                    if (futuresViewModel.Client.Code == (int)SpecialClients.Uno)
                    {
                        var futuresWithFixed = await GetContracts(true);
                        futuresViewModel.FinalResult = FinalResult(futuresWithFixed, futuresViewModel.FinalResult);
                    }
                }
                else
                {
                    return futuresViewModel.Capital;
                }
            }
            return futuresViewModel.FinalResult;
        }

        private async Task<int> UpdateRefeerFuture(FuturesViewModel futures, decimal gain) 
        {
            decimal refeerPerc = (decimal)1.01;
            if (futures.FinalResult == (decimal)0.00) 
            {
                futures.FinalResult = futures.Capital + (gain * refeerPerc);
            }
            else 
            {
                futures.FinalResult += (gain * refeerPerc);
            }
            _context.Futures.Update(futures);
            return await _context.SaveChangesAsync();
        }

        public async Task<FuturesViewModel> FuturesByNumber(int cNumber)
        {
            return await _context.Futures.FirstOrDefaultAsync(f => f.ContractNumber == cNumber);
        }

        public async Task<int> UpdateFinalResultFixed(List<FuturesViewModel> futuresViewModel)
        {
            foreach(var f in futuresViewModel)
            {
                if(f.Refeer.HasValue)
                {
                    var cl = await _context.Clients.FirstOrDefaultAsync(c => c.Id == f.Refeer.Value);
                    var refeerFuture = await FuturesByNumber(cl.Code);
                    await UpdateRefeerFuture(refeerFuture, f.LastGain);
                }
            }
            _context.UpdateRange(futuresViewModel);
            return await _context.SaveChangesAsync();
        }
    }
}
