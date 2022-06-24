using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Application.DTO
{
    public class AdminCreate
    {
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Retype Password")]
        public string ReTypePassword { get; set; }
    }
}
