namespace BusinessObject.Models
{
    public class Menu
    {
        public Menu()
        {
            ChildMenus = new HashSet<Menu>();
            Users = new HashSet<User>();
        }
        public string MenuId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string TabTitle { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public int Order { get; set; }
        private string? _pId;
        public string? ParentMenuId { get { return string.IsNullOrEmpty(_pId) ? null : _pId; } set { this._pId = value; } }
        public Menu? ParentMenu { get; set; }
        public ICollection<Menu> ChildMenus { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
