using Domain.Validation;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public BaseEntity(string name)
        {
            ValidateName(name);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public void ValidateName(string name)
        {
            ExceptionValidation.When(string.IsNullOrEmpty(name), "The name is required." );

            ExceptionValidation.When(name.Length < 3, "The name must be 3 caracters or more.");

            Name = name;
        }

        public void Update(string name)
        {
            ValidateName(name);
        }
    }
}
