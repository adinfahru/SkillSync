using Microsoft.AspNetCore.Mvc;
using SkillSyncAPI.DTOs;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Controllers;

[Route("api/projects/{projectId}/assignments")]
[ApiController]
public class ProjectAssignmentsController : ControllerBase
{
    private readonly IProjectAssignmentService _assignmentService;

    public ProjectAssignmentsController(IProjectAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<AssignmentResponseDto>>>> GetProjectAssignments(
        Guid projectId
    )
    {
        try
        {
            var assignments = await _assignmentService.GetProjectAssignmentsAsync(projectId);
            return Ok(new ApiResponse<List<AssignmentResponseDto>>(assignments));
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<List<AssignmentResponseDto>>(
                    StatusCodes.Status400BadRequest,
                    ex.Message
                )
            );
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AssignmentResponseDto>>> CreateAssignment(
        Guid projectId,
        [FromBody] CreateAssignmentDto dto
    )
    {
        try
        {
            var assignment = await _assignmentService.CreateAssignmentAsync(projectId, dto);
            return Ok(new ApiResponse<AssignmentResponseDto>(assignment));
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<AssignmentResponseDto>(StatusCodes.Status400BadRequest, ex.Message)
            );
        }
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<ApiResponse<AssignmentResponseDto>>> UpdateAssignment(
        Guid projectId,
        Guid userId,
        [FromBody] UpdateAssignmentDto dto
    )
    {
        try
        {
            var assignment = await _assignmentService.UpdateAssignmentAsync(projectId, userId, dto);
            if (assignment == null)
                return NotFound(
                    new ApiResponse<AssignmentResponseDto>(
                        StatusCodes.Status404NotFound,
                        "Assignment not found"
                    )
                );

            return Ok(new ApiResponse<AssignmentResponseDto>(assignment));
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<AssignmentResponseDto>(StatusCodes.Status400BadRequest, ex.Message)
            );
        }
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<ApiResponse<DeleteAssignmentResponseDto>>> DeleteAssignment(
        Guid projectId,
        Guid userId
    )
    {
        try
        {
            var result = await _assignmentService.DeleteAssignmentAsync(projectId, userId);
            if (!result)
                return NotFound(
                    new ApiResponse<DeleteAssignmentResponseDto>(
                        StatusCodes.Status404NotFound,
                        "Assignment not found"
                    )
                );

            var response = new DeleteAssignmentResponseDto
            {
                ProjectId = projectId,
                UserId = userId,
                DeletedAt = DateTime.UtcNow,
                Message = "User removed from project successfully",
            };

            return Ok(new ApiResponse<DeleteAssignmentResponseDto>(response));
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<DeleteAssignmentResponseDto>(
                    StatusCodes.Status400BadRequest,
                    ex.Message
                )
            );
        }
    }
}
