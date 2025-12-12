using Microsoft.AspNetCore.Mvc;
using SkillSyncAPI.DTOs.Users;
using SkillSyncAPI.Services.Interfaces;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return Unauthorized(
                new ApiResponse<object>(
                    StatusCodes.Status401Unauthorized,
                    "Invalid email or password"
                )
            );

        return Ok(
            new ApiResponse<LoginResponseDto>(StatusCodes.Status200OK, "Login successful", result)
        );
    }
}
