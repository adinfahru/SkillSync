namespace SkillSyncAPI.Models;

public class TalentSkills
{
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public SkillLevel Level { get; set; }

    // Many TalentSkills punya 1 TalentProfile
    public TalentProfiles? TalentProfile { get; set; }

    // Many TalentSkills punya 1 Skill
    public Skills? Skill { get; set; }
}

public enum SkillLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Expert,
}
