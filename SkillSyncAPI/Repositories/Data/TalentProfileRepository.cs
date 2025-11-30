using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories.Data;

public class TalentProfileRepository : Repository<TalentProfile>, ITalentProfileRepository
{
    public TalentProfileRepository(SkillSyncDbContext context)
        : base(context) { }

    public async Task<TalentProfile?> GetByIdWithUserAsync(Guid userId, CancellationToken ct)
    {
        return await _context
            .TalentProfile.Include(tp => tp.User)
            .FirstOrDefaultAsync(tp => tp.UserId == userId, ct);
    }

    public async Task<IEnumerable<TalentProfile>> GetAllWithUserAsync(
        CancellationToken ct,
        string? search = null
    )
    {
        var query = _context.TalentProfile.Include(tp => tp.User).AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(tp =>
                tp.FirstName.Contains(search)
                || tp.LastName.Contains(search)
                || tp.Department.Contains(search)
            );

        return await query.ToListAsync(ct);
    }
}
