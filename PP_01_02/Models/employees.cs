﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_01_02.Models
{
    public class employees
    {
        [Key]
        public int employee_id { get; set; }

        public string last_name { get; set; }

        public string name { get; set; }

        public string sur_name { get; set; }

        public string position { get; set; }
    }
}