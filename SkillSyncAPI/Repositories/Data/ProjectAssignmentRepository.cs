using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Data;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Data;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Repositories;

public class ProjectAssignmentRepository
    : Repository<ProjectAssignment>,
        IProjectAssignmentRepository
{
    public ProjectAssignmentRepository(SkillSyncDbContext context)
        : base(context) { }

    public async Task<IEnumerable<ProjectAssignment>> GetProjectAssignmentsAsync(
        Guid projectId,
        CancellationToken ct
    )
    {
        return await _context
            .ProjectAssignment.Where(pa => pa.ProjectId == projectId)
            .Include(pa => pa.User)
            .OrderBy(pa => pa.AssignedAt)
            .ToListAsync(ct);
    }

    public async Task<ProjectAssignment?> GetAssignmentAsync(
        Guid projectId,
        Guid userId,
        CancellationToken ct
    )
    {
        return await _context
            .ProjectAssignment.Where(pa => pa.ProjectId == projectId && pa.UserId == userId)
            .Include(pa => pa.User)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ExistsAsync(Guid projectId, Guid userId, CancellationToken ct)
    {
        return await _context.ProjectAssignment.AnyAsync(
            pa => pa.ProjectId == projectId && pa.UserId == userId,
            ct
        );
    }

    public async Task DeleteAssignmentAsync(Guid projectId, Guid userId, CancellationToken ct)
    {
        var assignment = await GetAssignmentAsync(projectId, userId, ct);
        if (assignment != null)
        {
            _context.ProjectAssignment.Remove(assignment);
            await _context.SaveChangesAsync(ct);
        }
    }
}
