using Application.Products.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Products.Handlers
{
    public class GetProducByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProducByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id) ?? throw new ApplicationException("Entity could not be found.");
            return product;
        }
    }
}
