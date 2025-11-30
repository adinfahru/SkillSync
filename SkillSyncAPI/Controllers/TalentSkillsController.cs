using Microsoft.AspNetCore.Mvc;
using SkillSyncAPI.DTOs;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Controllers;

[ApiController]
[Route("api/talents/{talentId}/skills")]
public class TalentSkillsController : ControllerBase
{
    private readonly ITalentSkillService _talentSkillService;

    public TalentSkillsController(ITalentSkillService talentSkillService)
    {
        _talentSkillService = talentSkillService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTalentSkills(Guid talentId)
    {
        var skills = await _talentSkillService.GetTalentSkillsAsync(talentId);
        return Ok(new ApiResponse<List<TalentSkillResponseDto>>(skills));
    }

    [HttpPost]
    public async Task<IActionResult> AddTalentSkill(Guid talentId, [FromBody] AddTalentSkillDto dto)
    {
        var skill = await _talentSkillService.AddTalentSkillAsync(talentId, dto);
        return Ok(
            new ApiResponse<TalentSkillResponseDto>(
                StatusCodes.Status201Created,
                "Skill added to talent successfully",
                skill
            )
        );
    }

    [HttpPut("{skillId}")]
    public async Task<IActionResult> UpdateTalentSkill(
        Guid talentId,
        Guid skillId,
        [FromBody] UpdateTalentSkillDto dto
    )
    {
        var skill = await _talentSkillService.UpdateTalentSkillAsync(talentId, skillId, dto);
        if (skill == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Talent skill not found")
            );
        return Ok(
            new ApiResponse<TalentSkillResponseDto>(
                StatusCodes.Status200OK,
                "Skill level updated successfully",
                skill
            )
        );
    }

    [HttpDelete("{skillId}")]
    public async Task<IActionResult> DeleteTalentSkill(Guid talentId, Guid skillId)
    {
        var success = await _talentSkillService.DeleteTalentSkillAsync(talentId, skillId);
        if (!success)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Talent skill not found")
            );
        return Ok(
            new ApiResponse<DeleteTalentSkillResponseDto>(
                StatusCodes.Status200OK,
                "Skill removed from talent successfully",
                new DeleteTalentSkillResponseDto
                {
                    Message = "Skill removed from talent successfully",
                }
            )
        );
    }
}
