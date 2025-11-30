using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories.Data;

public class TalentSkillRepository : Repository<TalentSkill>, ITalentSkillRepository
{
    public TalentSkillRepository(SkillSyncDbContext context)
        : base(context) { }

    public async Task<IEnumerable<TalentSkill>> GetTalentSkillsAsync(
        Guid userId,
        CancellationToken ct
    )
    {
        return await _context
            .TalentSkill.Include(ts => ts.Skill)
            .Where(ts => ts.UserId == userId)
            .ToListAsync(ct);
    }

    public async Task<TalentSkill?> GetTalentSkillAsync(
        Guid userId,
        Guid skillId,
        CancellationToken ct
    )
    {
        return await _context
            .TalentSkill.Include(ts => ts.Skill)
            .FirstOrDefaultAsync(ts => ts.UserId == userId && ts.SkillId == skillId, ct);
    }

    public async Task DeleteAsync(Guid userId, Guid skillId, CancellationToken ct)
    {
        var talentSkill = await GetTalentSkillAsync(userId, skillId, ct);
        if (talentSkill != null)
        {
            _context.TalentSkill.Remove(talentSkill);
            await _context.SaveChangesAsync(ct);
        }
    }
}
