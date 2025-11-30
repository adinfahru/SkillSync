using SkillSyncAPI.Data;
using SkillSyncAPI.DTOs;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;

namespace SkillSyncAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private static readonly List<Guid> ValidRoleIds = new()
    {
        SkillSyncDataSeeder.AdminRoleId,
        SkillSyncDataSeeder.HRRoleId,
        SkillSyncDataSeeder.ProjectManagerRoleId,
        SkillSyncDataSeeder.TalentRoleId,
    };

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponseDto>> GetAllUsersAsync(string? search = null)
    {
        var ct = CancellationToken.None;
        var users = await _userRepository.GetAllWithRoleAsync(ct);

        if (!string.IsNullOrEmpty(search))
            users = users
                .Where(u =>
                    u.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)
                    || u.Email.Contains(search, StringComparison.OrdinalIgnoreCase)
                )
                .ToList();

        return users.Select(MapToResponseDto).ToList();
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdWithRoleAsync(id, CancellationToken.None);
        return user != null ? MapToResponseDto(user) : null;
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto)
    {
        var ct = CancellationToken.None;

        if (!ValidRoleIds.Contains(dto.RoleId))
            throw new InvalidOperationException("Role not found");

        var users = await _userRepository.GetAllAsync(ct);
        if (users.Any(u => u.Email == dto.Email))
            throw new InvalidOperationException("Email already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            RoleId = dto.RoleId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _userRepository.CreateAsync(user, ct);
        return MapToResponseDto(user);
    }

    public async Task<UserResponseDto?> UpdateUserAsync(Guid id, UpdateUserDto dto)
    {
        var ct = CancellationToken.None;
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null)
            return null;

        if (dto.RoleId.HasValue)
        {
            if (!ValidRoleIds.Contains(dto.RoleId.Value))
                throw new InvalidOperationException("Role not found");
            user.RoleId = dto.RoleId.Value;
        }

        if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
        {
            var users = await _userRepository.GetAllAsync(ct);
            if (users.Any(u => u.Email == dto.Email))
                throw new InvalidOperationException("Email already exists");
            user.Email = dto.Email;
        }

        if (dto.IsActive.HasValue)
            user.IsActive = dto.IsActive.Value;

        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
        return MapToResponseDto(user);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id, CancellationToken.None);
        if (user == null)
            return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }

    private UserResponseDto MapToResponseDto(User user) =>
        new()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            RoleId = user.RoleId,
            RoleName = user.Role?.Name.ToString() ?? string.Empty,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };
}
