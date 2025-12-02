using SkillSyncAPI.DTOs.Projects;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailHandler _emailHandler;

    public ProjectService(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IEmailHandler emailHandler
    )
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _emailHandler = emailHandler;
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

        // Send email notification (fire and forget)
        _ = SendProjectCreationEmailAsync(project);

        return MapToResponseDto(project);
    }

    private async Task SendProjectCreationEmailAsync(Project project)
    {
        try
        {
            var emailBody =
                $@"
                <h2>New Project Created</h2>
                <p><strong>Project Name:</strong> {project.Name}</p>
                <p><strong>Description:</strong> {project.Description}</p>
                <p><strong>Status:</strong> {project.Status}</p>
                <p><strong>Created At:</strong> {project.CreatedAt:yyyy-MM-dd HH:mm:ss}</p>
                <hr>
                <p>Project ID: {project.Id}</p>
            ";

            var emailDto = new EmailDto(
                To: "admin@skillsync.local",
                Subject: $"New Project Created: {project.Name}",
                Body: emailBody
            );

            await _emailHandler.SendEmailAsync(emailDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send project creation email: {ex.Message}");
        }
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
