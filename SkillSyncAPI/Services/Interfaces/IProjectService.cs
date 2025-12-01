using SkillSyncAPI.DTOs.Projects;

namespace SkillSyncAPI.Services.Interfaces;

public interface IProjectService
{
    Task<List<ProjectResponseDto>> GetAllProjectsAsync(string? search = null);
    Task<ProjectResponseDto?> GetProjectByIdAsync(Guid id);
    Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto dto);
    Task<ProjectResponseDto?> UpdateProjectAsync(Guid id, UpdateProjectDto dto);
    Task<bool> DeleteProjectAsync(Guid id);
}
