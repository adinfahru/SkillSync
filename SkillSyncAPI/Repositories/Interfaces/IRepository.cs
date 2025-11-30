namespace SkillSyncAPI.Repositories.Interfaces;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
