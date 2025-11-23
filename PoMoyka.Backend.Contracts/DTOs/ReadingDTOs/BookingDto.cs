using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public DateTime BookedTime { get; set; }
        public BookingStatus Status { get; set; }
        public Guid UserId { get; set; }
        public Guid CenterServiceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

