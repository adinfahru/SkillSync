namespace SkillSyncAPI.Models;

public class Projects
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Active;
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum ProjectStatus
{
    Active,
    Inactive,
    Completed,
    OnHold,
    Cancelled,
}
