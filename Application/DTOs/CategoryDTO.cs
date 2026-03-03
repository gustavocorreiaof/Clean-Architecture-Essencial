using Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CategoryDTO : BaseDTO
    {
        [Required(ErrorMessage = "The name was required.")]
        [MinLength(3, ErrorMessage = "The name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "The name must be at most 100 characters long.")]
        public string Name { get; set; }
    }
}
