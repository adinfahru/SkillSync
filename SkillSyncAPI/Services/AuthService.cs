using System.Security.Claims;
using SkillSyncAPI.DTOs.Users;
using SkillSyncAPI.Models;
using SkillSyncAPI.Repositories.Interfaces;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashHandler _hashHandler;
    private readonly ITokenHandler _tokenHandler;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserRepository userRepository,
        IHashHandler hashHandler,
        ITokenHandler tokenHandler,
        IConfiguration configuration
    )
    {
        _userRepository = userRepository;
        _hashHandler = hashHandler;
        _tokenHandler = tokenHandler;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        // Cari user berdasarkan email
        var users = await _userRepository.GetAllWithRoleAsync(CancellationToken.None);
        var user = users.FirstOrDefault(u => u.Email == dto.Email);

        if (user == null)
            return null; // User tidak ditemukan

        // Verifikasi password
        if (!_hashHandler.ValidateHash(dto.Password, user.Password))
            return null; // Password salah

        // Generate JWT token
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role?.Name.ToString() ?? string.Empty),
            new Claim("RoleId", user.RoleId.ToString()),
            new Claim("UserId", user.Id.ToString()),
        };

        var tokenString = _tokenHandler.Access(claims);
        var durationInMinutes = int.Parse(_configuration["Jwt:DurationInMinutes"] ?? "5");
        var expiresIn = (int)TimeSpan.FromMinutes(durationInMinutes).TotalSeconds;

        return new LoginResponseDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            RoleName = user.Role?.Name.ToString() ?? string.Empty,
            Token = tokenString,
            ExpiresIn = expiresIn,
        };
    }
}
