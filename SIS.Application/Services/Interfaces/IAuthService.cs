using Microsoft.AspNetCore.Identity;
using SIS.Application.DTOs.Auth;

namespace SIS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task LogoutAsync();
    }
}
