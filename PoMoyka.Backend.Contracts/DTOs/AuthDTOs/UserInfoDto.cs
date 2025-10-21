using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.AuthDTOs
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}