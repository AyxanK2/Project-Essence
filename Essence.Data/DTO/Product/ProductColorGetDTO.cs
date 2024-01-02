using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Product
{
    public class ProductColorGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductColorSizeDTO> Sizes { get; set; }
    }
}
