using Domain.Validation;

namespace Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        protected BaseEntity(Guid id)
        {
            ValidateId(id);
            Id = id;
        }

        public Guid Id { get; private set; }

        public void ValidateId(Guid id)
        {
            ExceptionValidation.When(id == Guid.Empty, "The Id is required.");
            Id = id;
        }
    }
}
