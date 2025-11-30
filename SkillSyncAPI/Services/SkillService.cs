using SkillSyncAPI.DTOs;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;

namespace SkillSyncAPI.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;

    public SkillService(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<List<SkillResponseDto>> GetAllSkillsAsync(string? search = null)
    {
        var skills = await _skillRepository.GetAllAsync(CancellationToken.None, search);
        return skills.Select(MapToResponseDto).ToList();
    }

    public async Task<SkillResponseDto?> GetSkillByIdAsync(Guid id)
    {
        var skill = await _skillRepository.GetByIdAsync(id, CancellationToken.None);
        return skill != null ? MapToResponseDto(skill) : null;
    }

    public async Task<SkillResponseDto> CreateSkillAsync(CreateSkillDto dto)
    {
        var skill = new Skill
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Category = dto.Category,
        };

        await _skillRepository.CreateAsync(skill, CancellationToken.None);
        return MapToResponseDto(skill);
    }

    public async Task<SkillResponseDto?> UpdateSkillAsync(Guid id, UpdateSkillDto dto)
    {
        var skill = await _skillRepository.GetByIdAsync(id, CancellationToken.None);
        if (skill == null)
            return null;

        if (!string.IsNullOrEmpty(dto.Name))
            skill.Name = dto.Name;

        if (!string.IsNullOrEmpty(dto.Category))
            skill.Category = dto.Category;

        await _skillRepository.UpdateAsync(skill);
        return MapToResponseDto(skill);
    }

    public async Task<bool> DeleteSkillAsync(Guid id)
    {
        var skill = await _skillRepository.GetByIdAsync(id, CancellationToken.None);
        if (skill == null)
            return false;

        await _skillRepository.DeleteAsync(skill);
        return true;
    }

    private SkillResponseDto MapToResponseDto(Skill skill) =>
        new()
        {
            Id = skill.Id,
            Name = skill.Name,
            Category = skill.Category,
        };
}
