using Microsoft.AspNetCore.Mvc;
using SkillSyncAPI.DTOs.Projects;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects([FromQuery] string? search = null)
    {
        var projects = await _projectService.GetAllProjectsAsync(search);
        return Ok(new ApiResponse<List<ProjectResponseDto>>(projects));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Project not found")
            );
        return Ok(new ApiResponse<ProjectResponseDto>(project));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
    {
        try
        {
            var project = await _projectService.CreateProjectAsync(dto);
            return CreatedAtAction(
                nameof(GetProjectById),
                new { id = project.Id },
                new ApiResponse<ProjectResponseDto>(
                    StatusCodes.Status201Created,
                    "Project created successfully",
                    project
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse<object>(StatusCodes.Status400BadRequest, ex.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectDto dto)
    {
        var project = await _projectService.UpdateProjectAsync(id, dto);
        if (project == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Project not found")
            );
        return Ok(
            new ApiResponse<ProjectResponseDto>(
                StatusCodes.Status200OK,
                "Project updated successfully",
                project
            )
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var success = await _projectService.DeleteProjectAsync(id);
        if (!success)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Project not found")
            );
        return Ok(
            new ApiResponse<DeleteProjectResponseDto>(
                StatusCodes.Status200OK,
                "Project deleted successfully",
                new DeleteProjectResponseDto { Message = "Project deleted successfully" }
            )
        );
    }
}
