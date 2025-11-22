using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.UpdateDTOs
{
    public class TypeServiceUpdateDto
    {
        public Guid ServiceId { get; set; }
        public CarType CarType { get; set; }
    }
}

