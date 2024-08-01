using System.Linq.Expressions;

namespace BarberShop.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task Save(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);
    }
}
