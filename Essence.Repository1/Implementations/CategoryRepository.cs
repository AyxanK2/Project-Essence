using Essence.Data.DTO.Category;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Essence.Repository1.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllWithDetails()
        {
          return await _context.Categories
                .Include(x=>x.TopCategory)
                .Include(x=>x.SubCategory)
                .Where(x => x.DeletedAt == null).ToListAsync();
        }

        public async Task<List<Category>> GetSubCategories()
        {
            return await _context.Categories
                .Where(x=>x.DeletedAt == null)
                .Where(x=>x.TopCategory != null)
                .ToListAsync();
        }

        public async Task<List<Category>> GetTopCategories()
        {
            return await _context.Categories
                .Where(x => x.DeletedAt == null)
                .Where(x => x.TopCategory == null)
                .ToListAsync();
        }

        public async Task UpdateAsync(Category category, CategoryPutDTO model)
        {
            category.UpdatedAt = DateTime.Now;
            category.Name = model.Name;
            category.TopCategoryId = model.TopCategoryId;
            category.Image = model.Image;
            await _context.SaveChangesAsync();
        }
    }
}
