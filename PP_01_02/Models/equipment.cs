using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_01_02.Models
{
    public class equipment
    {
        [Key]
        public int equipment_id { get; set; }

        public string name { get; set; }

        public int type_id { get; set; }

        public string serial_number { get; set; }

        public string manufacturer { get; set; }

        public DateTime installation_date { get; set; }
    }
}