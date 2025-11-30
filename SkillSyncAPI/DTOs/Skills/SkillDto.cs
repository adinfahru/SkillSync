namespace SkillSyncAPI.DTOs;

public class CreateSkillDto
{
    public required string Name { get; set; }
    public required string Category { get; set; }
}

public class UpdateSkillDto
{
    public string? Name { get; set; }
    public string? Category { get; set; }
}

public class SkillResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}

public class DeleteSkillResponseDto
{
    public string Message { get; set; } = string.Empty;
}
