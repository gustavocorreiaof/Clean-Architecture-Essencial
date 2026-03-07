using Application.DTOs;
using Application.Interfaces;
using Application.Products.Commands;
using Application.Products.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(ProductDTO productDto)
        {
            ProductCreateCommand productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto) ?? throw new Exception("ProductCreateCommand is null");
            
            await _mediator.Send(productCreateCommand);            
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            GetProductsQuery productsQuery = new GetProductsQuery();

            IEnumerable<Product> entities = await _mediator.Send(productsQuery);    

            return _mapper.Map<IEnumerable<ProductDTO>>(entities);
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {

            GetProductByIdQuery getProductByIdQuery = new GetProductByIdQuery(id) ?? throw new Exception("GetProductByIdQuery is null");

            Product product = await _mediator.Send(getProductByIdQuery) ?? throw new Exception("Product not found");

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task RemoveProductAsync(Guid id)
        {

            ProductRemoveCommand productRemoveCommand = new ProductRemoveCommand(id) ?? throw new Exception("ProductRemoveCommand is null");

            await _mediator.Send(productRemoveCommand);
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            ProductUpdateCommand productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto) ?? throw new Exception("ProductUpdateCommand is null");
            
            await _mediator.Send(productUpdateCommand);
        }
    }
}
