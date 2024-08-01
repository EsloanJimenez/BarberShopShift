using BarberShop.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BarberShop.Infrastructure.Core
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _entities;
        protected BaseRepository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public virtual async Task Save(TEntity entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task Update(TEntity entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.AnyAsync(filter);
        }

        public virtual async Task<TEntity> Get(int Id)
        {
            return await _entities.FindAsync(Id);
        }

        public virtual async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.Where(filter).ToListAsync();
        }


        public virtual async Task SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
