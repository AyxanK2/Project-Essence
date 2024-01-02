using Essence.Data.DTO;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Implementations
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task UpdateAsync(Brand brand, BrandPutDTO model)
        {
            brand.Name = model.Name;
            brand.Image = model.Image;
            brand.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
