namespace SkillSyncAPI.Models;

public class Skill
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }

    // 1 Skill bisa dimiliki banyak TalentSkills
    public ICollection<TalentSkill>? TalentSkill { get; set; }
}
