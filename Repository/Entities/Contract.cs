using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Contract
    {
        public Contract()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Details = new HashSet<ContractDetail>();
        }
        [Key]
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Code { get; set; } = null!;
        public DateTime? ActivatedDate { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public DateTime? InvoiceExportedDate { get; set; }
        public bool IsNew { get; set; }
        public bool IsSigned { get; set; }
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public StatusCategory StatusCategory { get; set; } = null!;
        public ICollection<ContractDetail> Details { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
    }
}
