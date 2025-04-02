using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_01_02.Models
{
    public class calibration
    {
        [Key]
        public int calibration_id { get; set; }

        public int equipment_id { get; set; }

        public DateOnly calibration_date { get; set; }

        public int calibrated_by { get; set; }

        public string calibration_result { get; set; }

        public string notes { get; set; }
    }
}