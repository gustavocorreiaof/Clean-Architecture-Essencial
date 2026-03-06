using Application.Products.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException("Entity could not be found.");

            return await _productRepository.RemoveAsync(product);
        }
    }
}
