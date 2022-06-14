using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Detail
{
    public class DetailCreate
    {
        public string ISBN13 { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Pages { get; set; }
        public int Rating { get; set; }
        public int Sales { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
