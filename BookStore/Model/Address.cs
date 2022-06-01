using System;

namespace Bookstore.Model
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string AdressBox { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phonenumber { get; set; }
    }
}