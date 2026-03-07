using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
