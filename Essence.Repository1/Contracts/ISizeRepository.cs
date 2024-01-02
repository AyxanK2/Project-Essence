using Essence.Data.DTO;
using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface ISizeRepository : IGenericRepository<Size>
    {
        public Task UpdateAsync(Size size, SizeGetDTO model);
    }

}
