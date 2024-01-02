using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.Models
{
    public class Product :BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(80)]
        public string Slug { get; set; }
        public double SalePrice { get; set; }
        public double Discount { get; set; } = 0;
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public int TopCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public Category TopCategory { get; set; }
        public Category SubCategory { get; set; }
        public Brand Brand { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductImage> ProductImages { get; set; }


        public double GetPrice()
        {
            return SalePrice - Discount;
        }
    }
}
