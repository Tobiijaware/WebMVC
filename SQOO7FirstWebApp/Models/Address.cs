using System;
using System.ComponentModel.DataAnnotations;

namespace SQOO7FirstWebApp.Models
{
    public class Address
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [StringLength(150)]
        public string Street { get; set; }

        [StringLength(150)]
        public string City { get; set; }

        [StringLength(150)]
        public string COuntry { get; set; }
    }
}