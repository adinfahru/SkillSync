using Microsoft.AspNetCore.Mvc;
using SkillSyncAPI.DTOs;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillsController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSkills([FromQuery] string? search = null)
    {
        var skills = await _skillService.GetAllSkillsAsync(search);
        return Ok(new ApiResponse<List<SkillResponseDto>>(skills));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkillById(Guid id)
    {
        var skill = await _skillService.GetSkillByIdAsync(id);
        if (skill == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Skill not found")
            );
        return Ok(new ApiResponse<SkillResponseDto>(skill));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSkill([FromBody] CreateSkillDto dto)
    {
        var skill = await _skillService.CreateSkillAsync(dto);
        return CreatedAtAction(
            nameof(GetSkillById),
            new { id = skill.Id },
            new ApiResponse<SkillResponseDto>(StatusCodes.Status201Created, "Created", skill)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSkill(Guid id, [FromBody] UpdateSkillDto dto)
    {
        var skill = await _skillService.UpdateSkillAsync(id, dto);
        if (skill == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Skill not found")
            );
        return Ok(
            new ApiResponse<SkillResponseDto>(
                StatusCodes.Status200OK,
                "Skill updated successfully",
                skill
            )
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(Guid id)
    {
        var success = await _skillService.DeleteSkillAsync(id);
        if (!success)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Skill not found")
            );
        return Ok(
            new ApiResponse<DeleteSkillResponseDto>(
                StatusCodes.Status200OK,
                "Skill deleted successfully",
                new DeleteSkillResponseDto { Message = "Skill deleted successfully" }
            )
        );
    }
}
