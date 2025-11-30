using SkillSyncAPI.DTOs;

namespace SkillSyncAPI.Services.Interfaces;

public interface ITalentProfileService
{
    Task<List<TalentResponseDto>> GetAllTalentsAsync(string? search = null);
    Task<TalentResponseDto?> GetTalentByIdAsync(Guid userId);
    Task<TalentResponseDto?> UpdateTalentAsync(Guid userId, UpdateTalentDto dto);
    Task<TalentResponseDto?> UpdateAvailabilityAsync(Guid userId, UpdateAvailabilityDto dto);
}
