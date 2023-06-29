using BusinessObject.Models.Base;

namespace BusinessObject.Models
{
    public class TaxCategory : Category<int, string>
    {
        public TaxCategory()
        {
            Packages = new HashSet<Package>();
        }
        public ICollection<Package> Packages { get; set; }
    }
}
