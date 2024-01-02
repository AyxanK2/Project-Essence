using Essence.Data.DTO.Color;
using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        public void Update(Color color, ColorGetDTO model);
    }

}
