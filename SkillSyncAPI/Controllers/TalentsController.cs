using Microsoft.AspNetCore.Mvc;
using SkillSyncAPI.DTOs;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TalentsController : ControllerBase
{
    private readonly ITalentProfileService _talentProfileService;

    public TalentsController(ITalentProfileService talentProfileService)
    {
        _talentProfileService = talentProfileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTalents([FromQuery] string? search = null)
    {
        var talents = await _talentProfileService.GetAllTalentsAsync(search);
        return Ok(new ApiResponse<List<TalentResponseDto>>(talents));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTalentById(Guid id)
    {
        var talent = await _talentProfileService.GetTalentByIdAsync(id);
        if (talent == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Talent not found")
            );
        return Ok(new ApiResponse<TalentResponseDto>(talent));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTalent(Guid id, [FromBody] UpdateTalentDto dto)
    {
        var talent = await _talentProfileService.UpdateTalentAsync(id, dto);
        if (talent == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Talent not found")
            );
        return Ok(
            new ApiResponse<TalentResponseDto>(
                StatusCodes.Status200OK,
                "Talent updated successfully",
                talent
            )
        );
    }

    [HttpPut("{id}/availability")]
    public async Task<IActionResult> UpdateAvailability(
        Guid id,
        [FromBody] UpdateAvailabilityDto dto
    )
    {
        var talent = await _talentProfileService.UpdateAvailabilityAsync(id, dto);
        if (talent == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "Talent not found")
            );
        return Ok(
            new ApiResponse<TalentResponseDto>(
                StatusCodes.Status200OK,
                "Availability status updated successfully",
                talent
            )
        );
    }
}
