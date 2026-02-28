using Domain.Entities.Base;
using Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Category")]
    public sealed class Category : BaseEntity
    {
        public Category(Guid id, string name) : base(id)
        {
            ValidateName(name);
        }

        public ICollection<Product> Products { get; private set; }
        public string Name { get; private set; }

        public void ValidateName(string name)
        {
            ExceptionValidation.When(string.IsNullOrEmpty(name), "The name is required.");

            ExceptionValidation.When(name.Length < 3, "The name must be 3 caracters or more.");

            Name = name;
        }

        public void Update(string name)
        {
            ValidateName(name);
        }
    }
}
