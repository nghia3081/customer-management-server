using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Package
    {
        public Package()
        {
            CreatedDate = DateTime.Now;
            ContractDetails = new HashSet<ContractDetail>();
        }
        [Key]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int LicenseNumberQuantity { get; set; }
        public int LicenseMonthQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TaxCategoryId { get; set; }
        [ForeignKey(nameof(TaxCategoryId))]
        public TaxCategory TaxCategory { get; set; } = null!;
        public ICollection<ContractDetail> ContractDetails { get; set; }

    }
}
