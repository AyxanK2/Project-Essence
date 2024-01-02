using Essence.Data.DTO.Color;
using Essence.Data.Models;
using Essence.Repository1.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Implementations
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(ApplicationContext context) : base(context)
        {
        }

        public void Update(Color color, ColorGetDTO model)
        {
            color.UpdatedAt = DateTime.Now;
            color.Name = model.Name;
            color.Code = model.Code;
        }
    }
}
