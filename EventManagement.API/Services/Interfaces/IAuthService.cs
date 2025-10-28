using System.Threading.Tasks;
using EventManagement.API.DTOs.Auth;

namespace EventManagement.API.Services.Guiderfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
