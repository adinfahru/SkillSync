using SkillSyncAPI.DTOs;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;

namespace SkillSyncAPI.Services;

public class TalentProfileService : ITalentProfileService
{
    private readonly ITalentProfileRepository _talentProfileRepository;

    public TalentProfileService(ITalentProfileRepository talentProfileRepository)
    {
        _talentProfileRepository = talentProfileRepository;
    }

    public async Task<List<TalentResponseDto>> GetAllTalentsAsync(string? search = null)
    {
        var talents = await _talentProfileRepository.GetAllWithUserAsync(
            CancellationToken.None,
            search
        );
        return talents.Select(MapToResponseDto).ToList();
    }

    public async Task<TalentResponseDto?> GetTalentByIdAsync(Guid userId)
    {
        var talent = await _talentProfileRepository.GetByIdWithUserAsync(
            userId,
            CancellationToken.None
        );
        return talent != null ? MapToResponseDto(talent) : null;
    }

    public async Task<TalentResponseDto?> UpdateTalentAsync(Guid userId, UpdateTalentDto dto)
    {
        var talent = await _talentProfileRepository.GetByIdAsync(userId, CancellationToken.None);
        if (talent == null)
            return null;

        if (!string.IsNullOrEmpty(dto.FirstName))
            talent.FirstName = dto.FirstName;

        if (!string.IsNullOrEmpty(dto.LastName))
            talent.LastName = dto.LastName;

        if (!string.IsNullOrEmpty(dto.Department))
            talent.Department = dto.Department;

        if (!string.IsNullOrEmpty(dto.Bio))
            talent.Bio = dto.Bio;

        talent.UpdatedAt = DateTime.UtcNow;
        await _talentProfileRepository.UpdateAsync(talent);

        return MapToResponseDto(talent);
    }

    public async Task<TalentResponseDto?> UpdateAvailabilityAsync(
        Guid userId,
        UpdateAvailabilityDto dto
    )
    {
        var talent = await _talentProfileRepository.GetByIdAsync(userId, CancellationToken.None);
        if (talent == null)
            return null;

        if (!Enum.TryParse<TalentStatus>(dto.AvailabilityStatus, true, out var status))
            throw new InvalidOperationException(
                "Invalid availability status. Valid values: Available, Unavailable, OnLeave, Inactive"
            );

        talent.AvailabilityStatus = status;
        talent.UpdatedAt = DateTime.UtcNow;
        await _talentProfileRepository.UpdateAsync(talent);

        return MapToResponseDto(talent);
    }

    private TalentResponseDto MapToResponseDto(TalentProfile talent) =>
        new()
        {
            UserId = talent.UserId,
            FirstName = talent.FirstName,
            LastName = talent.LastName,
            FullName = $"{talent.FirstName} {talent.LastName}",
            Department = talent.Department,
            AvailabilityStatus = talent.AvailabilityStatus.ToString(),
            Bio = talent.Bio,
            CreatedAt = talent.CreatedAt,
            UpdatedAt = talent.UpdatedAt,
        };
}
