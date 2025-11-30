using SkillSyncAPI.DTOs;

namespace SkillSyncAPI.Services.Interfaces;

public interface ITalentSkillService
{
    Task<List<TalentSkillResponseDto>> GetTalentSkillsAsync(Guid userId);
    Task<TalentSkillResponseDto> AddTalentSkillAsync(Guid userId, AddTalentSkillDto dto);
    Task<TalentSkillResponseDto?> UpdateTalentSkillAsync(
        Guid userId,
        Guid skillId,
        UpdateTalentSkillDto dto
    );
    Task<bool> DeleteTalentSkillAsync(Guid userId, Guid skillId);
}
