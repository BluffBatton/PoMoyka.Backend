using Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? AvatarPath { get; set; }
        public Role Role { get; set; } = Role.Client;

        public virtual Car ?Car { get; set; }
        public virtual Center ?Center { get; set; }
        public virtual ICollection<Statement> ?Statements { get; set; }
        public virtual ICollection<Booking> ?Bookings { get; set; }
         
    }
}
