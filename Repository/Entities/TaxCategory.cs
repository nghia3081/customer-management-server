using Repository.Entities.Base;

namespace Repository.Entities
{
    public class TaxCategory : Category<int, string>
    {
        public TaxCategory()
        {
            Packages = new HashSet<Package>();
        }
        public virtual ICollection<Package> Packages { get; set; }
    }
}
