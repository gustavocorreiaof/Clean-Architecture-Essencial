using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllCategoriesAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<Product> GetProductCategoryAsync(Guid id);
        Task<Product> UpdateAsync(Product product);
        Task<Product> RemoveAsync(Product product);
    }
}
