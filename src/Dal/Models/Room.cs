﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class Room
    {
        public int Id { get; set; }

        //Data Validation
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        public List<Department> Departments { get; set; }
    }
}
