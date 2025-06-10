using HospitalAPI.DTOs;
using HospitalAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDTO request, CancellationToken token)
    {
        await _authService.RegisterUserAsync(request, token);
        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO request, CancellationToken token)
    {
        var tokens = await _authService.LoginAsync(request, token);
        return Ok(tokens);
    }

    [HttpPost("refresh")]
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken, CancellationToken token)
    {
        var newAccessToken = await _authService.RefreshTokenAsync(refreshToken, token);
        return Ok(new { accessToken = newAccessToken });
    }
}