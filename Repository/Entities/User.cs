using System.ComponentModel.DataAnnotations;

namespace Repository.Entities
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
        [Key]
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
