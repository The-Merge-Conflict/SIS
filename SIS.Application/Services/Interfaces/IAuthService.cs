using Microsoft.AspNetCore.Identity;
using SIS.Application.DTOs.Auth;

namespace SIS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequest request);
        Task<SignInResult> LoginAsync(LoginRequest request);
        Task LogoutAsync();
    }
}
