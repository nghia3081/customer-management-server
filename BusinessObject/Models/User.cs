using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Menus = new HashSet<Menu>();
            IsAdmin = false;
            IsBlocked = false;
        }
        public virtual string Username { get; set; } = null!;
        public virtual string Password { get; set; } = null!;
        public virtual string Phone { get; set; } = null!;
        [EmailAddress]
        public virtual string Email { get; set; } = null!;
        public virtual string FullName { get; set; } = null!;
        public virtual bool IsAdmin { get; set; }
        public virtual bool IsBlocked { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }

    public class UpdateUserInformationDto : User
    {
        public new string? Password { get; set; }
    }
   public class ResetPasswordDto : UpdateUserInformationDto
    {
        public new string? Phone { get; set; }
        public new string? FullName { get; set; }
    }
    public class UpdateUserPermissionDto : ResetPasswordDto
    {
      
        public new string? Email { get; set; }
     
    }
    public class ChangePasswordDto
    {
        public string? UserName { get; set; }
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
