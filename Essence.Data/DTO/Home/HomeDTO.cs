using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Home
{
    public class HomeDTO
    {
        public List<Models.Product> Products { get; set; }
        public List<Models.Category> Categories { get; set; }
        public List<Models.Brand> Brands { get; set; }
    }
}
