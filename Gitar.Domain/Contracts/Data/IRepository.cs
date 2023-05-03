using Gitar.Domain.Common;
using Gitar.Domain.Models;
using System.Linq.Expressions;

namespace Gitar.Domain.Contracts.Data;

public interface IRepository<TEntity, TKey> where TEntity : IEntityBase
{
    Task<Response<TEntity>> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<IList<TEntity>> GetAsync(Func<TEntity, bool>? predicate = null);
    Task<TEntity?> GetByKeyAsync(TKey key);
    Task DeleteAsync(TKey key);
}
