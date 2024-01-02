using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.Models
{
    public class ProductColorSize :BaseEntity
    {
        public int ProductColorId { get; set; }
        public int SizeId { get; set; }
        public int Count { get; set; } = 0;
        public ProductColor ProductColor { get; set; }
        public Size Size { get; set; }
    }
}
