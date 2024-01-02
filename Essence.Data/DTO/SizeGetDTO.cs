using Essence.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO
{
    public class SizeGetDTO :SizePostDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
