namespace SkillSyncAPI.DTOs;

public class CreateProjectDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string Status { get; set; } = "Active";
}

public class UpdateProjectDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
}

public class ProjectResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class DeleteProjectResponseDto
{
    public string Message { get; set; } = string.Empty;
}
