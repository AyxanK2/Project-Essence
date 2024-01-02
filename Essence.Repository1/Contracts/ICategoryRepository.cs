using Essence.Data.DTO.Category;
using Essence.Data.Models;

namespace Essence.Repository1.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task UpdateAsync(Category category, CategoryPutDTO model);
        public Task<List<Category>> GetAllWithDetails();
        public Task<List<Category>> GetTopCategories();
        public Task<List<Category>> GetSubCategories();
    }

}
