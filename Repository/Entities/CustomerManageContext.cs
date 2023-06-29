using Microsoft.EntityFrameworkCore;

namespace Repository.Entities
{
    public partial class CustomerManageContext : DbContext
    {
        public CustomerManageContext()
        {
        }

        public CustomerManageContext(DbContextOptions<CustomerManageContext> options)
            : base(options)
        {
        }
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<ContractDetail> ContractDetails { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Package> Packages { get; set; } = null!;
        public DbSet<StatusCategory> StatusCategories { get; set; } = null!;
        public DbSet<TaxCategory> TaxCategories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                        modelBuilder.Entity<Customer>().HasIndex(customer => customer.TaxCode).IsUnique(unique: true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
