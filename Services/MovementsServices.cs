using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traders.Data;
using Traders.Models;

namespace Traders.Services
{
    public class MovementsServices : IMovementsServices
    {
        private readonly ApplicationDbContext _context;

        public MovementsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateMovement(MovementsViewModel model)
        {
            _context.Add(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<MovementsViewModel>> GetAllMovements()
        {
            return await _context.Movements.ToListAsync();
        }

        public async Task<MovementsViewModel> GetMovements(Guid? id)
        {
            return await _context.Movements.FirstOrDefaultAsync(m => m.Id == id);
        }

        public bool MovementsViewModelExists(Guid id)
        {
            return _context.Movements.Any(e => e.Id == id);
        }

        public async Task<int> RemoveMovements()
        {
            try
            {
                var movements = await _context.Movements.Where(m => m.DateMov < DateTime.Today.AddDays(-90)).ToListAsync();
                _context.Movements.RemoveRange(movements);
                return await _context.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
        }
    }

}
