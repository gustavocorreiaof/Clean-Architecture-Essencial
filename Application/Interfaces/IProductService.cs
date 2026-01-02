using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task CreateProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(ProductDTO productDto);
        Task RemoveProductAsync(Guid id);
    }
}
