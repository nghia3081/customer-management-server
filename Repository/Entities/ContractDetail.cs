using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class ContractDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public string PackageCode { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public int LicenseNumberQuantity { get; set; }
        public int LicenseMonthQuantity { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public int TaxValue { get; set; }
        [ForeignKey(nameof(ContractId))]
        public virtual Contract Contract { get; set; } = null!;
        [ForeignKey(nameof(PackageCode))]
        public virtual Package? Package { get; set; }
    }
}
