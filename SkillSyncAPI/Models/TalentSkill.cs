namespace SkillSyncAPI.Models;

public class TalentSkill
{
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public SkillLevel Level { get; set; }

    // Many TalentSkills punya 1 TalentProfile
    public TalentProfile? TalentProfile { get; set; }

    // Many TalentSkills punya 1 Skill
    public Skill? Skill { get; set; }
}

public enum SkillLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Expert,
}
