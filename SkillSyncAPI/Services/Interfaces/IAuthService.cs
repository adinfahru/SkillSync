using SkillSyncAPI.DTOs.Users;

namespace SkillSyncAPI.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
}
