using SkillSyncAPI.DTOs;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;

namespace SkillSyncAPI.Services;

public class ProjectAssignmentService : IProjectAssignmentService
{
    private readonly IProjectAssignmentRepository _assignmentRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public ProjectAssignmentService(
        IProjectAssignmentRepository assignmentRepository,
        IProjectRepository projectRepository,
        IUserRepository userRepository
    )
    {
        _assignmentRepository = assignmentRepository;
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task<List<AssignmentResponseDto>> GetProjectAssignmentsAsync(Guid projectId)
    {
        var assignments = await _assignmentRepository.GetProjectAssignmentsAsync(
            projectId,
            CancellationToken.None
        );
        return assignments.Select(MapToResponseDto).ToList();
    }

    public async Task<AssignmentResponseDto> CreateAssignmentAsync(
        Guid projectId,
        CreateAssignmentDto dto
    )
    {
        var ct = CancellationToken.None;

        var project = await _projectRepository.GetByIdAsync(projectId, ct);
        if (project == null)
            throw new InvalidOperationException("Project not found");

        var user = await _userRepository.GetByIdAsync(dto.UserId, ct);
        if (user == null)
            throw new InvalidOperationException("User not found");

        // Check if assignment already exists
        var exists = await _assignmentRepository.ExistsAsync(projectId, dto.UserId, ct);
        if (exists)
            throw new InvalidOperationException("User is already assigned to this project");

        var assignment = new ProjectAssignment
        {
            ProjectId = projectId,
            UserId = dto.UserId,
            RoleOnProject = dto.RoleOnProject,
            AssignedAt = DateTime.UtcNow,
        };

        await _assignmentRepository.CreateAsync(assignment, ct);
        assignment.User = user;
        return MapToResponseDto(assignment);
    }

    public async Task<AssignmentResponseDto?> UpdateAssignmentAsync(
        Guid projectId,
        Guid userId,
        UpdateAssignmentDto dto
    )
    {
        var assignment = await _assignmentRepository.GetAssignmentAsync(
            projectId,
            userId,
            CancellationToken.None
        );
        if (assignment == null)
            return null;

        if (!string.IsNullOrEmpty(dto.RoleOnProject))
            assignment.RoleOnProject = dto.RoleOnProject;

        await _assignmentRepository.UpdateAsync(assignment);
        return MapToResponseDto(assignment);
    }

    public async Task<bool> DeleteAssignmentAsync(Guid projectId, Guid userId)
    {
        var assignment = await _assignmentRepository.GetAssignmentAsync(
            projectId,
            userId,
            CancellationToken.None
        );
        if (assignment == null)
            return false;

        await _assignmentRepository.DeleteAssignmentAsync(
            projectId,
            userId,
            CancellationToken.None
        );
        return true;
    }

    private AssignmentResponseDto MapToResponseDto(ProjectAssignment assignment) =>
        new()
        {
            ProjectId = assignment.ProjectId,
            UserId = assignment.UserId,
            UserName = assignment.User?.UserName ?? string.Empty,
            RoleOnProject = assignment.RoleOnProject,
            AssignedAt = assignment.AssignedAt,
            CompletedAt = assignment.CompletedAt,
        };
}
