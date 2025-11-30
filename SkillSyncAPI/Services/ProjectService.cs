using SkillSyncAPI.DTOs;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;

namespace SkillSyncAPI.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ProjectResponseDto>> GetAllProjectsAsync(string? search = null)
    {
        var projects = await _projectRepository.GetAllAsync(CancellationToken.None, search);
        return projects.Select(MapToResponseDto).ToList();
    }

    public async Task<ProjectResponseDto?> GetProjectByIdAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id, CancellationToken.None);
        return project != null ? MapToResponseDto(project) : null;
    }

    public async Task<ProjectResponseDto> CreateProjectAsync(CreateProjectDto dto)
    {
        if (!Enum.TryParse<ProjectStatus>(dto.Status, true, out var status))
            throw new InvalidOperationException(
                "Invalid project status. Valid values: Active, Inactive, Completed, OnHold, Cancelled"
            );

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Status = status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _projectRepository.CreateAsync(project, CancellationToken.None);
        return MapToResponseDto(project);
    }

    public async Task<ProjectResponseDto?> UpdateProjectAsync(Guid id, UpdateProjectDto dto)
    {
        var project = await _projectRepository.GetByIdAsync(id, CancellationToken.None);
        if (project == null)
            return null;

        if (!string.IsNullOrEmpty(dto.Name))
            project.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Description))
            project.Description = dto.Description;

        if (!string.IsNullOrEmpty(dto.Status))
        {
            if (!Enum.TryParse<ProjectStatus>(dto.Status, true, out var status))
                throw new InvalidOperationException(
                    "Invalid project status. Valid values: Active, Inactive, Completed, OnHold, Cancelled"
                );
            project.Status = status;
        }

        project.UpdatedAt = DateTime.UtcNow;
        await _projectRepository.UpdateAsync(project);
        return MapToResponseDto(project);
    }

    public async Task<bool> DeleteProjectAsync(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id, CancellationToken.None);
        if (project == null)
            return false;

        await _projectRepository.DeleteAsync(project);
        return true;
    }

    private ProjectResponseDto MapToResponseDto(Project project) =>
        new()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status.ToString(),
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,
        };
}
