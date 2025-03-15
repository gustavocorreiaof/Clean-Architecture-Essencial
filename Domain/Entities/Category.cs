using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Category")]
    internal class Category : BaseEntity
    {
        public ICollection<Product> Products { get; set; }
    }
}
