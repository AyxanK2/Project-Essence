using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Product
{
    public class ProductGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<string> Images { get; set; }
        public List<ProductColorGetDTO> Colors { get; set; }
        public string TopCategory { get; set; }
        public string? SubCategory { get; set; }
        public string Brand { get; set; }
        public double SalePrice { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
