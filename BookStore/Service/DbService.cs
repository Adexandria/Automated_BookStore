using Bookstore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service
{
    public class DbService : DbContext
    {
        public DbService(DbContextOptions<DbService> options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCart> Carts { get; set; }

    }
}
