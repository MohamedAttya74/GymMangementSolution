using GymMangement.DAL.Repositories.Interfaces;
using GymMangement.DbContexts;
using GymMangement.Models;
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
        private readonly GymDbContext dbContext;

        public PlanRepository()
        {
            dbContext = new GymDbContext();
        }
        public async Task<IEnumerable<Plan>> GetAllAsync(bool Tracking = false, CancellationToken ct = default)
        {
            IQueryable<Plan> Query = Tracking? dbContext.Plans : dbContext.Plans.AsNoTracking();
            return await Query.ToListAsync(ct);
        }
        public async Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default)
        {
             return await dbContext.Plans.FindAsync(id, ct);
        }
        public async Task<int> AddAsync(Plan plan, CancellationToken ct = default)
        {
            dbContext.Plans.Add(plan); // Not Need To Use AddAsync 
            return await dbContext.SaveChangesAsync(ct);
        }
        public async Task<int> UpdateAsync(Plan plan, CancellationToken ct = default)
        {
           dbContext.Plans.Update(plan);
            return await dbContext.SaveChangesAsync(ct);
        }
        public async Task<int> DeleteAsync(Plan plan, CancellationToken ct = default)
        {
           dbContext.Plans.Remove(plan);
            return await dbContext.SaveChangesAsync(ct);
        }
    }
}
