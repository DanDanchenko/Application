namespace EventManagement.API.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public DateTime Expired { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
