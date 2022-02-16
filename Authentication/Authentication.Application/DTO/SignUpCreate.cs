using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Application.DTO
{
    public class SignUpCreate
    {
        [Required(ErrorMessage ="Enter FirstName"),StringLength(20)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Enter LastName"), StringLength(20)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Matriculation Number")]
        public string MatriculationNumber { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress]
        public string StudentEmail { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Retype Password")]
        public string ReTypePassword { get; set; }
        [Required(ErrorMessage = "Enter Faculty")]
        public string Faculty { get; set; }
        [Required(ErrorMessage = "Enter Department")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Enter Level")]
        public int Level { get; set; }

    }
}
