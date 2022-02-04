using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Domain.Entities
{
    public class Faculty
    {
        [Key]
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }
 
    }
}