using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expire { get; set; }
        public string Username { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
    }
}
