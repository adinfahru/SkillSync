namespace SkillSyncAPI.Models;

public class Roles
{
    public Guid Id { get; set; }
    public UserRole Name { get; set; }

    // 1 Role punya banyak Users
    public ICollection<Users>? Users { get; set; }
}

public enum UserRole
{
    Admin,
    HR,
    ProjectManager,
    Talent,
}
