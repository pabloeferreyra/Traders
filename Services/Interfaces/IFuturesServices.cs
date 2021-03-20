using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traders.Models;

namespace Traders.Services
{
    public interface IFuturesServices
    {
        public Task<List<FuturesViewModel>> GetFuturesForClient(Guid clientId);
        public Task<List<FuturesViewModel>> CalculateFutures(List<FuturesViewModel> futures);
        public Task<List<FuturesViewModel>> GetFutures(DateTime? date);
        public bool FuturesViewModelExists(Guid id);
        public Task<FuturesViewModel> GetFuture(Guid? id);
        public Task<int> GetLastContractNumber();
        public Task<List<FuturesUpdateViewModel>> GetFuturesUpdates(DateTime? startDate);
        public Task<bool> CreateFuture(FuturesViewModel model);
        public Task<int> CountContracts();
        public Task<List<FuturesViewModel>> GetContracts(bool fixRent);
        public Task<List<FuturesViewModel>> GetAllContracts();
        public SelectList Participations();
        public Task<List<FuturesUpdateViewModel>> GetFuturesUpdatesForMail(DateTime startDate, DateTime finishDate);
        public int GetParticipation(Guid? participationId);
        public decimal FinalResult(List<FuturesViewModel> futuresWithFixed, decimal finalResult);
        public decimal FixRentCalc(decimal capital, decimal? rentPercentage, DateTime startDate);
        public Task<int> CreateFutureUpdate(FuturesUpdateViewModel model);
        public Task<bool> FuturesUpdateViewModelExists(Guid id);
        public Task<bool> FuturesNumberExists(int cNumber);
        public Task<FuturesViewModel> FuturesByNumber(int cNumber);
        public Task<decimal> GetResult(FuturesViewModel futuresViewModel);
        public Task<int> UpdateFinalResultFixed(List<FuturesViewModel> futuresViewModel);
    }
}
