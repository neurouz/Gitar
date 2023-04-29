using System.Linq.Expressions;

namespace Gitar.Domain.Contracts.Data;

public interface IRepository<TEntity, TKey> where TEntity : IEntityBase
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> DeleteAsync(TKey key);
}
