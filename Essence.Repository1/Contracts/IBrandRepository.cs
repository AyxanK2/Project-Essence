using Essence.Data.DTO;
using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        public Task UpdateAsync(Brand brand,BrandPutDTO model);
    }
}
