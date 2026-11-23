using GymMangement.DAL.Data.DbContexts;
using GymMangement.DAL.Data.Models;
using GymMangement.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymMangement.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {

        private readonly GymDbContext _dbcontext;
        private readonly DbSet<TEntity> _Set;

        public GenericRepository(GymDbContext dbContext)
        {
            _dbcontext = dbContext;
            _Set = _dbcontext.Set<TEntity>();
        }

        public async Task<int> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            _Set.Add(entity);
            return await _dbcontext.SaveChangesAsync(ct);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            return _Set.AsNoTracking().AnyAsync(predicate, ct);
        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            _Set.Remove(entity);
            return await _dbcontext.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool Tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> Query = Tracking ? _Set : _Set.AsNoTracking();
            return await Query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _Set.FindAsync(id, ct);
        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            _Set.Update(entity);
            return await _dbcontext.SaveChangesAsync(ct);

        }
    }
}
