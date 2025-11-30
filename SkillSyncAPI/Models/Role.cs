namespace SkillSyncAPI.Models;

public class Role
{
    public Guid Id { get; set; }
    public UserRole Name { get; set; }

    // 1 Role punya banyak Users
    public ICollection<User>? User { get; set; }
}

public enum UserRole
{
    Admin,
    HR,
    ProjectManager,
    Talent,
}
