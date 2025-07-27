using Smockerie.Enum;

namespace Smockerie.DTO
{
    public class UserCreateDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; } = UserRole.Admin;
    }
}
