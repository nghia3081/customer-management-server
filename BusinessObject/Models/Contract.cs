namespace BusinessObject.Models
{
    public class Contract
    {
        public Contract()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Details = new HashSet<ContractDetail>();
            StatusId = 1;
        }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Code { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public DateTime? InvoiceExportedDate { get; set; }
        public bool IsNew { get; set; }
        public int StatusId { get; set; }
        public User? CreatedByNavigation { get; set; }
        public StatusCategory? StatusCategory { get; set; } = null!;
        public ICollection<ContractDetail> Details { get; set; }
        public Customer? Customer { get; set; } = null!;
    }
}
