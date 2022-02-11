using Authentication.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Authentication.Infrastructure.Service
{
    public class AuthDbService :IdentityDbContext<SignUp>
    {
        public AuthDbService(DbContextOptions<AuthDbService> options):base(options)
        {
  
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public override DbSet<SignUp> Users { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
