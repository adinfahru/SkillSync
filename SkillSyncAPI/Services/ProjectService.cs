using SkillSyncAPI.DTOs.Projects;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;

namespace SkillSyncAPI.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
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
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Status = ParseStatus(dto.Status),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _projectRepository.CreateAsync(project, CancellationToken.None);
        return MapToResponseDto(project);
    }

    // Helper method untuk clarity
    private ProjectStatus ParseStatus(string status)
    {
        if (Enum.TryParse<ProjectStatus>(status, true, out var result))
            return result;

        throw new ArgumentException(
            "Invalid status. Use: Active, Inactive, Completed, OnHold, Cancelled"
        );
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
