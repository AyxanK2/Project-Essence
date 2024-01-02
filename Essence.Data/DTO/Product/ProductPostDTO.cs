using Essence.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Product
{
    public class ProductPostDTO
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public string Description { get; set; }
        public double? Discount { get; set; }
        public int TopCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrandId { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<int> ColorIds { get; set; }
    }
}
