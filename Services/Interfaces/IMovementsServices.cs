using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traders.Models;

namespace Traders.Services
{
    public interface IMovementsServices
    {
        public Task<int> RemoveMovements();
        public Task<List<MovementsViewModel>> GetAllMovements();
        public Task<MovementsViewModel> GetMovements(Guid? id);
        public Task<int> CreateMovement(MovementsViewModel model);
        public bool MovementsViewModelExists(Guid id);
    }

}
