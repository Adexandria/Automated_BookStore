using System;
using System.ComponentModel.DataAnnotations;

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
        public string Phonenumber { get; set; }
        public Guid ProfileId { get; set; }
    }
}