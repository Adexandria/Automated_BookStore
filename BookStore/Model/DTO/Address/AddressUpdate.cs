using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Address
{
    public class AddressUpdate
    {
        public string AdressBox { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
