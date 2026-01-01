using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Tests
{
    public class ProductUnitTest
    {
        [Fact(DisplayName = "Create Product with valid state")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            string fictionName = "Product test name 1";
            Guid id = Guid.NewGuid();
            string description = "Product test description 1";
            decimal price = 9.99m;
            int stock = 100;
            string image = "product1.jpg";

            Action action = () => new Product(id, fictionName, description, price, stock, image);
            action.Should()
                .NotThrow<ExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product with negative price")]
        public void CreateProduct_WithNegativePrice_ResultValidationException()
        {
            string fictionName = "Product test name 1";
            Guid id = Guid.NewGuid();
            string description = "Product test description 1";
            decimal negativePrice = -9.99m;
            int stock = 100;
            string image = "product1.jpg";
            string expectedMessage = "Price must be grather than zero.";

            Action action = () => new Product(id, fictionName, description, negativePrice, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateProduct with too short name")]
        public void CreateProduct_WithShortNameValue_ResultValidationExceptionShortName()
        {
            string shotName = "IN";
            Guid id = Guid.NewGuid();
            string description = "Product test description 1";
            decimal negativePrice = 9.99m;
            int stock = 100;
            string image = "product1.jpg";
            string expectedMessage = "The name must be 3 caracters or more.";

            Action action = () => new Product(id, shotName, description, negativePrice, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateProduct with empty name")]
        public void CreateProduct_WithEmptyName_ResultExceptionNameRequired()
        {
            Guid id = Guid.NewGuid();
            string description = "Product test description 1";
            decimal negativePrice = 9.99m;
            int stock = 100;
            string image = "product1.jpg";
            string expectedMessage = "The name is required.";

            Action action = () => new Product(id, string.Empty, description, negativePrice, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateProduct with null name")]
        public void CreateProduct_WithNullName_ResultExceptionNameRequired()
        {
            Guid id = Guid.NewGuid();
            string description = "Product test description 1";
            decimal price = 9.99m;
            int stock = 100;
            string image = "product1.jpg";
            string expectedMessage = "The name is required.";

            Action action = () => new Product(id, null, description, price, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateProduct with empty description")]
        public void CreateProduct_WithEmptyDescription_ResultExceptionDescriptionRequired()
        {
            Guid id = Guid.NewGuid();
            string name = "Product test name 1";
            decimal negativePrice = 9.99m;
            int stock = 100;
            string image = "product1.jpg";
            string expectedMessage = "Description is required.";

            Action action = () => new Product(id, name, string.Empty, negativePrice, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateProduct with null description")]
        public void CreateProduct_WithNullDescription_ResultExceptionDescriptionRequired()
        {
            Guid id = Guid.NewGuid();
            string name = "Product test name 1";
            decimal negativePrice = 9.99m;
            int stock = 100;
            string image = "product1.jpg";
            string expectedMessage = "Description is required.";

            Action action = () => new Product(id, name, null, negativePrice, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "CreateProduct with too short description")]
        public void CreateProduct_WithTooShortDescription_ResultExceptionTooShortDescription()
        {
            Guid id = Guid.NewGuid();
            string name = "Product test name 1";
            string description = "1234";
            decimal negativePrice = 9.99m;
            int stock = 100;
            string image = "product1.jpg";
            string expectedMessage = "Description must be 5 caracters or more.";

            Action action = () => new Product(id, name, description, negativePrice, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Create Product with negative stock")]
        public void CreateProduct_WithNegativeStock_ResultValidationException()
        {
            string fictionName = "Product test name 1";
            Guid id = Guid.NewGuid();
            string description = "Product test description 1";
            decimal price = 9.99m;
            int stock = -100;
            string image = "product1.jpg";
            string expectedMessage = "Stock must be grather than zero.";

            Action action = () => new Product(id, fictionName, description, price, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Create Product with too long image")]
        public void CreateProduct_WithTooLongImage_ResultValidationException()
        {
            string fictionName = "Product test name 1";
            Guid id = Guid.NewGuid();
            string description = "Product test description 1";
            decimal price = 9.99m;
            int stock = 100;
            string image = "toooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo looooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnng imageeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee.jpg";
            string expectedMessage = "Image must be 250 caracters maximum.";

            Action action = () => new Product(id, fictionName, description, price, stock, image);
            action.Should()
                .Throw<ExceptionValidation>().WithMessage(expectedMessage);
        }
    }
}
