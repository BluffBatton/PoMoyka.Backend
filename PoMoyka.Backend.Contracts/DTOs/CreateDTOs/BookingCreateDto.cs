using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class BookingCreateDto
    {
        public DateTime BookedTime { get; set; }
        public Guid CenterServiceId { get; set; }
    }
}
