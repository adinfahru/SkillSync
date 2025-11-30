using SkillSyncAPI.Models;

namespace SkillSyncAPI.Repositories.Interfaces;

public interface ITalentSkillRepository : IRepository<TalentSkill>
{
    Task<IEnumerable<TalentSkill>> GetTalentSkillsAsync(Guid userId, CancellationToken ct);
    Task<TalentSkill?> GetTalentSkillAsync(Guid userId, Guid skillId, CancellationToken ct);
    Task DeleteAsync(Guid userId, Guid skillId, CancellationToken ct);
}
