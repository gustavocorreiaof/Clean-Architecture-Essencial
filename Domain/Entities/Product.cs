using Domain.Entities.Base;
using Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Product")]
    public sealed class Product : BaseEntity
    {
        public Product(Guid id, string name, string description, decimal price, int stock, string image) : base(id)
        {
            ValidateName(name);
            Validate(description, price, stock, image);
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        private void Validate(string desciption, decimal price, int stock, string image)
        {
            ExceptionValidation.When(string.IsNullOrEmpty(desciption), "Description is required.");
            ExceptionValidation.When(desciption.Length < 5, "Description must be 5 caracters or more.");
            ExceptionValidation.When(price < 0, "Price must be grather than zero.");
            ExceptionValidation.When(stock < 0, "Stock must be grather than zero.");
            ExceptionValidation.When(image?.Length > 250, "Image must be 250 caracters maximum.");

            Description = desciption;
            Price = price;
            Stock = stock;
            Image = image;
        }

        public void Update(string name, string description, decimal price, int stock, string image, Guid categoryId)
        {
            ValidateName(name);
            Validate(description, price, stock, image);
            CategoryId = categoryId;
        }

        public void ValidateName(string name)
        {
            ExceptionValidation.When(string.IsNullOrEmpty(name), "The name is required.");

            ExceptionValidation.When(name.Length < 3, "The name must be 3 caracters or more.");

            Name = name;
        }
    }
}
