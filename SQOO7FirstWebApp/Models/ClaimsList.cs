﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.Models
{
    public class ClaimsList
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TypeName { get; set; }
    }
}
