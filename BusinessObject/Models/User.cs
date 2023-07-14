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
}
