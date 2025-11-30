using SkillSyncAPI.Models;

namespace SkillSyncAPI.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdWithRoleAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllWithRoleAsync(CancellationToken cancellationToken);
}
