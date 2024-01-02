using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.Models
{
    public class Category :BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100),MaybeNull]
        public string? Image { get; set; }
        [MaybeNull]
        public int? TopCategoryId { get; set; }
        public Category TopCategory { get; set; }
        public List<Category> SubCategory { get; set; }
    }
}
