using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventManagement.API.Data;
using EventManagement.API.DTOs.Auth;
using EventManagement.API.Models;
using EventManagement.API.Services.Guiderfaces;
using BCrypt;

namespace EventManagement.API.Services
{
    public class AuthService : IAuthService
    {
        public EventsManagementContext _context;
        public ITokenService tokenService;

        public AuthService(EventsManagementContext context, ITokenService tService)
        {
            _context = context;
            tokenService = tService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto rDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == rDto.Email)) throw new Exception("User with this Email already exists");

            var user = new User { Id = Guid.NewGuid(), Email = rDto.Email, FullName = rDto.FullName, PasswordHash = BCrypt.Net.BCrypt.HashPassword(rDto.Password) };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var token = tokenService.CreateToken(user);

            return new AuthResponseDto { Token = token, Email = user.Email, FullName = user.FullName };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto lDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == lDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(lDto.Password, user.PasswordHash)) throw new Exception("Invalid email or password, Access Denied!");
            var token = tokenService.CreateToken(user);

            return new AuthResponseDto { Token = token, Email = user.Email, FullName = user.FullName };
        }
    }
}
