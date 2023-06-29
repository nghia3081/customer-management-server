using System.ComponentModel.DataAnnotations;

namespace Repository.Entities.Base
{
    public abstract class Category<TKey, TValue>

    {
        public Category()
        {
            CreatedDate = DateTime.Now;
        }
        [Key]
        public TKey Id { get; set; }
        public TValue Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
