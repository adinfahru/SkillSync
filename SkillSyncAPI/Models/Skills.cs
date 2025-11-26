namespace SkillSyncAPI.Models;

public class Skills
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; }
}
