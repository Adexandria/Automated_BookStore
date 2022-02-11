using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class SignUp:IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public override string Email { get; set; }
        public override string PasswordHash { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
        public string Matriculation_Number { get; set; }
    }
}
