using Domain.Entities;
using Domain.Interfaces;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;        
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await _context.categories.FindAsync(id); 
        }

        public async Task<Category> RemoveAsync(Category category)
        {
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
