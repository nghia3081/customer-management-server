
namespace BusinessObject.Models.Base
{
    public abstract class Category<TKey, TValue>

    {
        public Category()
        {
            CreatedDate = DateTime.Now;
        }
        public TKey Id { get; set; }
        public TValue Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
