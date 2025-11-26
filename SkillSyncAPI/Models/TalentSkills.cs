namespace SkillSyncAPI.Models;

public class TalentSkills
{
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public SkillLevel Level { get; set; }
}

public enum SkillLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Expert,
}
