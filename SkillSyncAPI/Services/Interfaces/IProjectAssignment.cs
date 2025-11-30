using SkillSyncAPI.DTOs;

namespace SkillSyncAPI.Services.Interfaces;

public interface IProjectAssignmentService
{
    Task<List<AssignmentResponseDto>> GetProjectAssignmentsAsync(Guid projectId);
    Task<AssignmentResponseDto> CreateAssignmentAsync(Guid projectId, CreateAssignmentDto dto);
    Task<AssignmentResponseDto?> UpdateAssignmentAsync(
        Guid projectId,
        Guid userId,
        UpdateAssignmentDto dto
    );
    Task<bool> DeleteAssignmentAsync(Guid projectId, Guid userId);
}
