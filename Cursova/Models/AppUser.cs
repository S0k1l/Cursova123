using Microsoft.AspNetCore.Identity;

namespace Cursova.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Buildind { get; set; }
        public string Apartment { get; set; }
        public string? DocumentUrl { get; set; }
        public ICollection<Statements> Statements { get; set; }
    }
}
