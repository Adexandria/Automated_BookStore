using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Domain.Entities
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        public string DepartmentName{ get; set; }

        [ForeignKey("FacultyId")]
        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}