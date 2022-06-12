using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model
{
    public class UserProfile
    {
        [Key]
        public Guid ProfileId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MatriculationNumber { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
        public string Id { get; set; }
       
    }
}
