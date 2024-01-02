using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Category
{
    public class CategoryGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string TopCategory { get; set; }
        public List<string> SubCategories { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
