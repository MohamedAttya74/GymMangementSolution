using GymMangement.DAL.Data.DbContexts;
using GymMangement.DAL.Repositories.Interfaces;
using GymMangement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.DAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext _dbContext;

        public PlanRepository(GymDbContext dbContext)
        {
            _dbContext =dbContext;
        }
        public async Task<IEnumerable<Plan>> GetAllAsync(bool Tracking = false, CancellationToken ct = default)
        {
            IQueryable<Plan> Query = Tracking? _dbContext.Plans : _dbContext.Plans.AsNoTracking();
            return await Query.ToListAsync(ct);
        }
        public async Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default)
        {
             return await _dbContext.Plans.FindAsync(id, ct);
        }
        public async Task<int> AddAsync(Plan plan, CancellationToken ct = default)
        {
            _dbContext.Plans.Add(plan); // Not Need To Use AddAsync 
            return await _dbContext.SaveChangesAsync(ct);
        }
        public async Task<int> UpdateAsync(Plan plan, CancellationToken ct = default)
        {
           _dbContext.Plans.Update(plan);
            return await _dbContext.SaveChangesAsync(ct);
        }
        public async Task<int> DeleteAsync(Plan plan, CancellationToken ct = default)
        {
           _dbContext.Plans.Remove(plan);
            return await _dbContext.SaveChangesAsync(ct);
        }
    }
}
