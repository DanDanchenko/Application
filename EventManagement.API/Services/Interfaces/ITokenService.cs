using EventManagement.API.Models;

namespace EventManagement.API.Services.Guiderfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
