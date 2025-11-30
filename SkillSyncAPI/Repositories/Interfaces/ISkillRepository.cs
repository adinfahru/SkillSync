using SkillSyncAPI.Models;

namespace SkillSyncAPI.Repositories.Interfaces;

public interface ISkillRepository : IRepository<Skill>
{
    Task<IEnumerable<Skill>> GetAllAsync(CancellationToken ct, string? search = null);
}
