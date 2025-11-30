namespace SkillSyncAPI.Models;

public class ProjectAssignment
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public required string RoleOnProject { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public Project? Project { get; set; }
    public User? User { get; set; }
}
