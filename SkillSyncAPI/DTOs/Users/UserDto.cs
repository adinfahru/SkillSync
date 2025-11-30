namespace SkillSyncAPI.DTOs;

public class CreateUserDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Guid RoleId { get; set; }
}

public class UpdateUserDto
{
    public string? Email { get; set; }
    public Guid? RoleId { get; set; }
    public bool? IsActive { get; set; }
}

public class UserResponseDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class DeleteUserResponseDto
{
    public string Message { get; set; } = string.Empty;
}
