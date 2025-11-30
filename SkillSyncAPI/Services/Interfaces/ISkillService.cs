using SkillSyncAPI.DTOs;

namespace SkillSyncAPI.Services.Interfaces;

public interface ISkillService
{
    Task<List<SkillResponseDto>> GetAllSkillsAsync(string? search = null);
    Task<SkillResponseDto?> GetSkillByIdAsync(Guid id);
    Task<SkillResponseDto> CreateSkillAsync(CreateSkillDto dto);
    Task<SkillResponseDto?> UpdateSkillAsync(Guid id, UpdateSkillDto dto);
    Task<bool> DeleteSkillAsync(Guid id);
}
