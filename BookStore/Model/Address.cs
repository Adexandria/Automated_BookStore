using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Model
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }
        public string AdressBox { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [ForeignKey("UserProfile")]
        public Guid ProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}