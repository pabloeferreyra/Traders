using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traders.Models;

namespace Traders.Services
{
    public interface IFuturesRetireServices 
    {
        public Task<IList<RetireFuturesViewModel>> GetAllRetires(int cNumber);
        public Task<int> Create(RetireFuturesViewModel retireFuturesViewModel);
        public bool RetireFuturesViewModelExists(Guid id);
    }
}
