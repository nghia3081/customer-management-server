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
        public decimal GrandTotal
        {
            get
            {
                var total = this.UnitPrice * this.Quantity;
                var discount = total * (decimal)this.Discount / 100;
                return total - discount;
            }
        }
        public Contract? Contract { get; set; } = null!;
        public Package? Package { get; set; }
    }
}
