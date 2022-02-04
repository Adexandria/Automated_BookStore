using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Infrastructure.Service
{
    public class AuthDbService :DbContext
    {
        public AuthDbService(DbContextOptions<AuthDbService> options ):base(options)
        {

        }
        public DbSet<SignUp> Users { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
