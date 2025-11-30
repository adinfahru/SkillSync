using SkillSyncAPI.Models;

namespace SkillSyncAPI.Repositories.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct, string? search = null);
}
