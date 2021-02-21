using System.Collections.Generic;
using Traders.Data;
using Traders.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Traders.Services
{
    public class FuturesRetireServices : IFuturesRetireServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IFuturesServices _futuresServices;
        public FuturesRetireServices(ApplicationDbContext context,
            IFuturesServices futuresServices)
        {
            _context = context;
            _futuresServices = futuresServices;
        }

        public async Task<int> Create(RetireFuturesViewModel retireFuturesViewModel)
        {
            retireFuturesViewModel.Id = Guid.NewGuid();
            var future = await _futuresServices.FuturesByNumber(retireFuturesViewModel.ContractNumber);
            var finalR = future.Capital + future.FinalResult;
            if(retireFuturesViewModel.RetireCapital > future.FinalResult)
            {
                future.FinalResult = 0;
                future.Capital = finalR - retireFuturesViewModel.RetireCapital;
            }
            else
            {
                future.FinalResult -= retireFuturesViewModel.RetireCapital;
            }
            _context.Futures.Update(future);
            _context.Add(retireFuturesViewModel);
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<RetireFuturesViewModel>> GetAllRetires(int cNumber)
        {
            return await _context.Retires.Where(r => r.ContractNumber == cNumber).ToListAsync();
        }

        public bool RetireFuturesViewModelExists(Guid id)
        {
            return _context.Retires.Any(e => e.Id == id);
        }
    }
}
