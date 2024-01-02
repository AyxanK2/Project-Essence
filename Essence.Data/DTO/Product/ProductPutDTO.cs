using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Product
{
    public class ProductPutDTO
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<string> Images { get; set; }
        public List<ProductColorGetDTO> Colors { get; set; }
        public int TopCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public double SalePrice { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<int> ColorIds { get; set; }
        public List<IFormFile> Files{ get; set; }
    }
}
