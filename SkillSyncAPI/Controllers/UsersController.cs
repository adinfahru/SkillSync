using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSyncAPI.DTOs;
using SkillSyncAPI.Services;
using SkillSyncAPI.Utilities;

namespace SkillSyncAPI.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllUsers([FromQuery] string? search = null)
    {
        var users = await _userService.GetAllUsersAsync(search);
        return Ok(new ApiResponse<List<UserResponseDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "User not found")
            );
        return Ok(new ApiResponse<UserResponseDto>(user));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return CreatedAtAction(
            nameof(GetUserById),
            new { id = user.UserId },
            new ApiResponse<UserResponseDto>(StatusCodes.Status201Created, "Created", user)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto dto)
    {
        var user = await _userService.UpdateUserAsync(id, dto);
        if (user == null)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "User not found")
            );
        return Ok(
            new ApiResponse<UserResponseDto>(
                StatusCodes.Status200OK,
                "User updated successfully",
                user
            )
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (!success)
            return NotFound(
                new ApiResponse<object>(StatusCodes.Status404NotFound, "User not found")
            );
        return Ok(
            new ApiResponse<DeleteUserResponseDto>(
                StatusCodes.Status200OK,
                "User deleted successfully",
                new DeleteUserResponseDto { Message = "User deleted successfully" }
            )
        );
    }
}
