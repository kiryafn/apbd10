using HospitalAPI.DTOs;

namespace HospitalAPI.Services.Abstractions;

public interface IAuthService
{
    Task RegisterUserAsync(UserDTO request, CancellationToken token);
    Task<AuthResponseDto> LoginAsync(UserDTO request, CancellationToken token);
    Task<string> RefreshTokenAsync(string refreshToken, CancellationToken token);
}