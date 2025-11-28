namespace SkillSyncAPI.Models;

public class Skills
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }

    // 1 Skill bisa dimiliki banyak TalentSkills
    public ICollection<TalentSkills>? TalentSkills { get; set; }
}
