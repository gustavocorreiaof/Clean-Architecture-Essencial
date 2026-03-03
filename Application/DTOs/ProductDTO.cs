using Application.DTOs.Base;
using Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs
{
    public class ProductDTO : BaseDTO
    {
        [Required(ErrorMessage = "The name was required.")]
        [MinLength(3, ErrorMessage = "The name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "The name must be at most 100 characters long.")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description was required.")]
        [MinLength(5, ErrorMessage = "The description must be at least 5 characters long.")]
        [MaxLength(200, ErrorMessage = "The description must be at most 200 characters long.")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The price was required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than 0.")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The stock was required.")]
        [Range(0, 9999, ErrorMessage = "The stock must be between 0 and 999.")]
        [DisplayName("Stock")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "The image was required.")]
        [MaxLength(200, ErrorMessage = "The image must be at most 200 characters long.")]
        [DisplayName("Image")]
        public string Image { get; set; }

        [DisplayName("Categories")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}