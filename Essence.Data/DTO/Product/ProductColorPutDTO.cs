using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Product
{
    public class ProductColorPutDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public List<int> SizeIds { get; set; }
    }
}
