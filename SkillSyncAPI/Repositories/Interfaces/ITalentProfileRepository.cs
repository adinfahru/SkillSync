using SkillSyncAPI.Models;

namespace SkillSyncAPI.Repositories.Interfaces;

public interface ITalentProfileRepository : IRepository<TalentProfile>
{
    Task<TalentProfile?> GetByIdWithUserAsync(Guid userId, CancellationToken ct);
    Task<IEnumerable<TalentProfile>> GetAllWithUserAsync(
        CancellationToken ct,
        string? search = null
    );
}
