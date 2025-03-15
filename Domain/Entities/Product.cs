using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Product")]
    internal class Product : BaseEntity
    {
        public string Description { get; set; }
        public decimal Price  { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
