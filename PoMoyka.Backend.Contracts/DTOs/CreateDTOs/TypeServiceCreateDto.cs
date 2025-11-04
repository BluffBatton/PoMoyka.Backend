using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class TypeServiceCreateDto
    {
        public Guid ServiceId { get; set; }

        public CarType CarType { get; set; }
    }
}
