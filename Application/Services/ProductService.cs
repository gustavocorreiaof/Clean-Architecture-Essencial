using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(ProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            await _repository.CreateAsync(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            IEnumerable<Product> entities = await _repository.GetAllProductAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(entities);
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            Product product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task RemoveProductAsync(Guid id)
        {
            Product product = await _repository.GetByIdAsync(id);
            await _repository.RemoveAsync(product);
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            await _repository.UpdateAsync(product);
        }
    }
}
