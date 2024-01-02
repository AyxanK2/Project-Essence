using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO
{
    public class SizePostDTO
    {
        [StringLength(50, ErrorMessage = "Size's length must be less than 50 symbols")]
        public string Name { get; set; }
        [StringLength(5, ErrorMessage = "Size's length must be less than 5 symbols")]
        public string ShortName { get; set; }
    }
}
