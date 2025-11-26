namespace SkillSyncAPI.Models;

public class TalentProfiles
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Department { get; set; }
    public TalentStatus AvailabilityStatus { get; set; }
    public required string Bio { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // 1 TalentProfile punya 1 User
    public Users? User { get; set; }
}

public enum TalentStatus
{
    Available,
    Unavailable,
    OnLeave,
    Inactive,
}
