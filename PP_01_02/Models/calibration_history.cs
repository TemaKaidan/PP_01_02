using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_01_02.Models
{
    public class calibration_history
    {
        [Key]
        public int history_id { get; set; }

        public int calibration_id { get; set; }

        public int updated_by { get; set; }

        public DateTime change_date { get; set; }

        public string description { get; set; }
    }
}