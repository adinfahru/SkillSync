using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories.Data;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(SkillSyncDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken ct, string? search = null)
    {
        var query = _context.Project.AsQueryable();

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

        return await query.ToListAsync(ct);
    }
}
