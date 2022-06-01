using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model
{
    public class BookDetail
    {
        [Key]
        public Guid DetailId { get; set; }
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