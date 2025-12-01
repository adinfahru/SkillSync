namespace SkillSyncAPI.Repositories;

public interface IUnitOfWork
{
    Task CommitTransactionAsync(Func<Task> action, CancellationToken cancellationToken);
    Task ClearTracksAsync(CancellationToken cancellationToken);
}
