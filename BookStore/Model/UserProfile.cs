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
        public Guid Id { get; set; }
        [ForeignKey("Address")]
        public Address AddressId { get; set; }
        public Address Address { get; set; }
    }
}
