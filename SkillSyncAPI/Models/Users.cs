namespace SkillSyncAPI.Models;

public class Users
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public uint? Otp { get; set; }
    public DateTime? Expired { get; set; }
    public bool IsUsed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    // 1 User punya 1 TalentProfile
    public TalentProfiles? TalentProfile { get; set; }

    // Many Users punya 1 Role
    public Roles? Role { get; set; }

    // 1 User bisa punya banyak ProjectAssignments
    public ICollection<ProjectAssignments>? ProjectAssignments { get; set; }
}
