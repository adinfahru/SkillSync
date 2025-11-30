using SkillSyncAPI.Models;

namespace SkillSyncAPI.Repositories.Interfaces;

public interface IProjectAssignmentRepository : IRepository<ProjectAssignment>
{
    Task<IEnumerable<ProjectAssignment>> GetProjectAssignmentsAsync(
        Guid projectId,
        CancellationToken ct
    );
    Task<ProjectAssignment?> GetAssignmentAsync(Guid projectId, Guid userId, CancellationToken ct);
    Task<bool> ExistsAsync(Guid projectId, Guid userId, CancellationToken ct);
    Task DeleteAssignmentAsync(Guid projectId, Guid userId, CancellationToken ct);
}
