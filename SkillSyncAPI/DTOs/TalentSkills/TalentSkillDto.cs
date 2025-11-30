namespace SkillSyncAPI.DTOs;

public class AddTalentSkillDto
{
    public required Guid SkillId { get; set; }
    public required string Level { get; set; }
}

public class UpdateTalentSkillDto
{
    public required string Level { get; set; }
}

public class TalentSkillResponseDto
{
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public string SkillName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
}

public class DeleteTalentSkillResponseDto
{
    public string Message { get; set; } = string.Empty;
}
