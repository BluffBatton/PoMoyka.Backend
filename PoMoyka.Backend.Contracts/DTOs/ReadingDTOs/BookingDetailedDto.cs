using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class BookingDetailedDto
    {
        public Guid Id { get; set; }
        public DateTime BookedTime { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // User info
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }

        // Center info
        public Guid CenterId { get; set; }
        public string CenterName { get; set; }
        public string CenterAddress { get; set; }

        // Service info
        public Guid CenterServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public CarType CarType { get; set; }
        public decimal Price { get; set; }

        // Transaction info (если есть)
        public Guid? TransactionId { get; set; }
        public decimal? TransactionAmount { get; set; }

        // Rating info (если есть)
        public Guid? RatingId { get; set; }
        public int? RatingValue { get; set; } // 1-5 (One=1, Two=2, Three=3, Four=4, Five=5)
    }
}

