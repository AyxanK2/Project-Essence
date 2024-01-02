using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.DTO.Color
{
    public class ColorPostDTO
    {
        [StringLength(50, ErrorMessage = "Color's length must be less than 50 symbols")]
        public string Name { get; set; }
        [StringLength(15, ErrorMessage = "Color's length must be less than 5 symbols")]
        public string Code { get; set; }
    }
}
