using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EventManagement.API.DTOs.Auth;
using EventManagement.API.Services;
using EventManagement.API.Services.Guiderfaces;

namespace EventManagement.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _Service;

        public AuthController(IAuthService service)
        {
            _Service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto rDto)
        {
            var response = await _Service.RegisterAsync(rDto);

            return Ok(response);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDto lDto)
        {
            var response = await _Service.LoginAsync(lDto);

            return Ok(response);
        }

    }
}
