namespace SkillSyncAPI.DTOs;

public class UpdateTalentDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
    public string? Bio { get; set; }
}

public class UpdateAvailabilityDto
{
    public required string AvailabilityStatus { get; set; }
}

public class TalentResponseDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string AvailabilityStatus { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
