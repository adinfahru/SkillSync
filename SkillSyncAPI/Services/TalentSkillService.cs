using SkillSyncAPI.DTOs;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;

namespace SkillSyncAPI.Services;

public class TalentSkillService : ITalentSkillService
{
    private readonly ITalentSkillRepository _talentSkillRepository;
    private readonly ITalentProfileRepository _talentProfileRepository;
    private readonly ISkillRepository _skillRepository;

    public TalentSkillService(
        ITalentSkillRepository talentSkillRepository,
        ITalentProfileRepository talentProfileRepository,
        ISkillRepository skillRepository
    )
    {
        _talentSkillRepository = talentSkillRepository;
        _talentProfileRepository = talentProfileRepository;
        _skillRepository = skillRepository;
    }

    public async Task<List<TalentSkillResponseDto>> GetTalentSkillsAsync(Guid userId)
    {
        var talentSkills = await _talentSkillRepository.GetTalentSkillsAsync(
            userId,
            CancellationToken.None
        );
        return talentSkills.Select(MapToResponseDto).ToList();
    }

    public async Task<TalentSkillResponseDto> AddTalentSkillAsync(
        Guid userId,
        AddTalentSkillDto dto
    )
    {
        var ct = CancellationToken.None;

        var talent = await _talentProfileRepository.GetByIdAsync(userId, ct);
        if (talent == null)
            throw new InvalidOperationException("Talent not found");

        var skill = await _skillRepository.GetByIdAsync(dto.SkillId, ct);
        if (skill == null)
            throw new InvalidOperationException("Skill not found");

        var existing = await _talentSkillRepository.GetTalentSkillAsync(userId, dto.SkillId, ct);
        if (existing != null)
            throw new InvalidOperationException("Talent already has this skill");

        if (!Enum.TryParse<SkillLevel>(dto.Level, true, out var level))
            throw new InvalidOperationException(
                "Invalid skill level. Valid values: Beginner, Intermediate, Advanced, Expert"
            );

        var talentSkill = new TalentSkill
        {
            UserId = userId,
            SkillId = dto.SkillId,
            Level = level,
        };

        await _talentSkillRepository.CreateAsync(talentSkill, ct);

        talentSkill.Skill = skill;
        return MapToResponseDto(talentSkill);
    }

    public async Task<TalentSkillResponseDto?> UpdateTalentSkillAsync(
        Guid userId,
        Guid skillId,
        UpdateTalentSkillDto dto
    )
    {
        var ct = CancellationToken.None;
        var talentSkill = await _talentSkillRepository.GetTalentSkillAsync(userId, skillId, ct);
        if (talentSkill == null)
            return null;

        if (!Enum.TryParse<SkillLevel>(dto.Level, true, out var level))
            throw new InvalidOperationException(
                "Invalid skill level. Valid values: Beginner, Intermediate, Advanced, Expert"
            );

        talentSkill.Level = level;
        await _talentSkillRepository.UpdateAsync(talentSkill);

        return MapToResponseDto(talentSkill);
    }

    public async Task<bool> DeleteTalentSkillAsync(Guid userId, Guid skillId)
    {
        var ct = CancellationToken.None;
        var talentSkill = await _talentSkillRepository.GetTalentSkillAsync(userId, skillId, ct);
        if (talentSkill == null)
            return false;

        await _talentSkillRepository.DeleteAsync(userId, skillId, ct);
        return true;
    }

    private TalentSkillResponseDto MapToResponseDto(TalentSkill ts) =>
        new()
        {
            UserId = ts.UserId,
            SkillId = ts.SkillId,
            SkillName = ts.Skill?.Name ?? string.Empty,
            Category = ts.Skill?.Category ?? string.Empty,
            Level = ts.Level.ToString(),
        };
}
