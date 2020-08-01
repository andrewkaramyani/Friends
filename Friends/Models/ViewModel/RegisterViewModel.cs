using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Friends.Models.Enums;

namespace Friends.Models.ViewModel
{
    public class RegisterViewModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Required]
        [EmailAddress]
        //to use clint side 
       // [Remote("ISEmailInUse","account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [DisplayName("Confirm Passowrd")]
        [Compare("Password", ErrorMessage ="Don't Match Passowrd")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
    }
}
