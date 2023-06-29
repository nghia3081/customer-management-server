using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Customer
    {
        public Customer()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Contracts = new HashSet<Contract>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string TaxCode { get; set; } = null!;
        public string? AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByNavigation { get; set; } = null!;
        public ICollection<Contract> Contracts { get; set; }

    }
}
