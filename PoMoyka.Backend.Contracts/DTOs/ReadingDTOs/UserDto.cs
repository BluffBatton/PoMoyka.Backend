using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
