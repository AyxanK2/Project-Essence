using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.Models
{
    public class ProductColor :BaseEntity
    {
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Color Color { get; set; }
        public List<ProductColorSize> ProductColorSizes { get; set; }
    }
}
