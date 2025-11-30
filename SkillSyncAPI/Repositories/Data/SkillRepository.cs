using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories.Data;

public class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(SkillSyncDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Skill>> GetAllAsync(CancellationToken ct, string? search = null)
    {
        var query = _context.Skill.AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(s => s.Name.Contains(search) || s.Category.Contains(search));

        return await query.ToListAsync(ct);
    }
}
