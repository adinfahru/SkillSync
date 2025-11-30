using SkillSyncAPI.DTOs;

namespace SkillSyncAPI.Services;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllUsersAsync(string? search = null);
    Task<UserResponseDto?> GetUserByIdAsync(Guid id);
    Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
    Task<UserResponseDto?> UpdateUserAsync(Guid id, UpdateUserDto dto);
    Task<bool> DeleteUserAsync(Guid id);
}
