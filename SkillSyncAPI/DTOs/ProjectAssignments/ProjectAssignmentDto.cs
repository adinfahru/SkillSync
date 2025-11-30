namespace SkillSyncAPI.DTOs;

public class CreateAssignmentDto
{
    public required Guid UserId { get; set; }
    public required string RoleOnProject { get; set; }
}

public class UpdateAssignmentDto
{
    public string? RoleOnProject { get; set; }
}

public class AssignmentResponseDto
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string RoleOnProject { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class DeleteAssignmentResponseDto
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DeletedAt { get; set; }
    public string Message { get; set; } = string.Empty;
}
