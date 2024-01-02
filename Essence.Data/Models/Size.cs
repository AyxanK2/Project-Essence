using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Data.Models
{
    public class Size:BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(5)]
        public string ShortName { get; set; }
    }

}
