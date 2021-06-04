using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Password mismatch")]
        public string ConfirmPassword { get; set; }

        public string Photo { get; set; }

        public Dictionary<char, string> ListOfGender { get; set; }

        public RegisterViewModel()
        {
            ListOfGender = new Dictionary<char, string>();
            ListOfGender.Add('m', "Male");
            ListOfGender.Add('f', "Female");
            ListOfGender.Add('o', "Others");
        }

        public bool IsAdmin { get; set; } = false;
    }
}
