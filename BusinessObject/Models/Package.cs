namespace BusinessObject.Models
{
    public class Package
    {
        public Package()
        {
            CreatedDate = DateTime.Now;
            ContractDetails = new HashSet<ContractDetail>();
        }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int LicenseNumberQuantity { get; set; }
        public int LicenseMonthQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TaxCategoryId { get; set; }
        public TaxCategory? TaxCategory { get; set; }
        public ICollection<ContractDetail> ContractDetails { get; set; }

    }
}
