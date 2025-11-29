using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        // Booking info
        public Guid BookingId { get; set; }
        public DateTime BookedTime { get; set; }
        public BookingStatus BookingStatus { get; set; }

        // User info
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }

        // Center info
        public Guid CenterId { get; set; }
        public string CenterName { get; set; }

        // Service info
        public string ServiceName { get; set; }
        public string CarType { get; set; }

        // Rating info (если есть)
        public Guid? RatingId { get; set; }
        public int? RatingValue { get; set; }
    }
}
