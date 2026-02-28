using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Tests
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Create Category with valid state")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            string fictionName = "Category test name 1";
            Guid id = Guid.NewGuid();

            Action action = () => new Category(id, fictionName);
            action.Should()
                .NotThrow<ExceptionValidation>();
        }

        [Fact(DisplayName = "Create Category with invalid state")]
        public void CreateCategory_WithInvalidParameters_ResultValidationException()
        {
            string fictionName = "Category test name 1";
            Guid id = Guid.Empty;
            string expectedMessage = "The Id is required.";

            Action action = () => new Category(id, fictionName);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateCategory with too short name")]
        public void CreateCategory_WithShortNameValue_ResultValidationExceptionShortName()
        {
            string shotName = "IN";
            Guid id = Guid.NewGuid();
            string expectedMessage = "The name must be 3 caracters or more.";

            Action action = () => new Category(id, shotName);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateCategory with empty name")]
        public void CreateCategory_WithEmptyName_ResultExceptionNameRequired()
        {
            string emptyName = string.Empty;
            Guid id = Guid.NewGuid();
            string expectedMessage = "The name is required.";

            Action action = () => new Category(id, emptyName);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateCategory with null name")]
        public void CreateCategory_WithNullName_ResultExceptionNameRequired()
        {
            Guid id = Guid.NewGuid();
            string expectedMessage = "The name is required.";

            Action action = () => new Category(id, null);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }
    }
}
