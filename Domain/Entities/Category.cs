using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Category")]
    public sealed class Category : BaseEntity
    {
        public Category(string name) : base(name) { }

        public ICollection<Product> Products { get; private set; }
    }
}
