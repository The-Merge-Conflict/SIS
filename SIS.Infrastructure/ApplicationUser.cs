using Microsoft.AspNetCore.Identity;

namespace SIS.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public static implicit operator ApplicationUser(SignInResult v)
        {
            throw new NotImplementedException();
        }
    }
}