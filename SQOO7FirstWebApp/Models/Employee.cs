using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.Models
{
    public class Employee : IdentityUser //extending the Identity domain class
    {
        [StringLength(35)]
        public string FirstName { get; set; }

        [StringLength(35)]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public Address Address { get; set; }
        public string AddressId { get; set; }

        public string Photo { get; set; }

    }
}
