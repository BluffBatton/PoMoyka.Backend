using Domain.Enums;

namespace Domain.Entities
{
    internal class User
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string AvatarPath { get; set; }
        public Role Role { get; set; }

        public Car Car { get; set; }
        public Statement Statement { get; set; }
        public Booking Booking { get; set; }
    }
}
