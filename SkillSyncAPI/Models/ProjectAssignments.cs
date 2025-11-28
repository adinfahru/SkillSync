namespace SkillSyncAPI.Models;

public class ProjectAssignments
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public required string RoleOnProject { get; set; }
    public ProjectAssignmentStatus Status { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Many ProjectAssignments punya 1 Project
    public Projects? Project { get; set; }

    // Many ProjectAssignments punya 1 User
    public Users? User { get; set; }
}

public enum ProjectAssignmentStatus
{
    Active,
    Inactive,
    Completed,
    Replaced,
    Dropped,
}
