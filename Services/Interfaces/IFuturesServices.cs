using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traders.Models;

namespace Traders.Services
{
    public interface IFuturesServices
    {
        public Task<List<FuturesViewModel>> GetFutures();
        public bool FuturesViewModelExists(Guid id);
        public Task<FuturesViewModel> GetFuture(Guid? id);
        public Task<int> GetLastContractNumber();
        public Task<List<FuturesUpdateViewModel>> GetFuturesUpdates(DateTime? startDate);
        public Task<bool> CreateFuture(FuturesViewModel model);
        public Task<int> CountContracts(bool fixRent);
        public Task<List<FuturesViewModel>> GetContracts(bool fixRent);
        public Task<List<FuturesViewModel>> GetAllContracts();
        public SelectList Participations();
        public Task<List<FuturesUpdateViewModel>> GetFuturesUpdatesForMail(DateTime startDate, DateTime finishDate);
        public int GetParticipation(Guid? participationId);
        public decimal FinalResult(List<FuturesViewModel> futuresWithFixed, decimal finalResult);
        public decimal FixRentCalc(decimal capital);
        public Task<int> CreateFutureUpdate(FuturesUpdateViewModel model);
        public Task<bool> FuturesUpdateViewModelExists(Guid id);
    }
}
