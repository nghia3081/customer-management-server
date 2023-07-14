namespace BusinessObject.Models
{
    public class ContractDetail
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public string PackageCode { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public int LicenseNumberQuantity { get; set; }
        public int LicenseMonthQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public int TaxValue { get; set; }
        public Contract? Contract { get; set; } = null!;
        public Package? Package { get; set; }
    }
}
