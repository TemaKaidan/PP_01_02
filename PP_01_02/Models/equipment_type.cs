using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_01_02.Models
{
    public class equipment_type
    {
        [Key]
        public int type_id { get; set; }

        public string type_name { get; set; }
    }
}