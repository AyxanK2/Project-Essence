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
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        public SizeRepository(ApplicationContext context) : base(context)
        {
        }
        public async Task UpdateAsync(Size size, SizeGetDTO model)
        {
            size.UpdatedAt = DateTime.Now;
            size.Name = model.Name;
            size.ShortName = model.ShortName;
            await _context.SaveChangesAsync();
        }
    }
}
