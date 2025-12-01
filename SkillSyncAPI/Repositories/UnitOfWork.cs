using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Data;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SkillSyncDbContext _context;

    public UnitOfWork(SkillSyncDbContext context)
    {
        _context = context;
    }

    public async Task CommitTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(
            cancellationToken
        );

        try
        {
            await action();
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public Task ClearTracksAsync(CancellationToken cancellationToken)
    {
        _context.ChangeTracker.Clear();
        return Task.CompletedTask;
    }
}
