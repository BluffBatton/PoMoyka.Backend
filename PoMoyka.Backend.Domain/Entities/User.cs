using PoMoyka.Backend.Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public Role Role { get; set; } = Role.Client;

        public virtual Car ?Car { get; set; }
        public virtual Center ?Center { get; set; }
        public virtual UserImage? UserImage { get; set; }
        public virtual ICollection<Statement> ?Statements { get; set; }
        public virtual ICollection<Booking> ?Bookings { get; set; }

        // Поля для авторизации
        public DateTime? LastLoginAt { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
