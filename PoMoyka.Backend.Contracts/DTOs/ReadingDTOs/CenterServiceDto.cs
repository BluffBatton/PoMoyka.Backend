namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class CenterServiceDto
    {
        public Guid Id { get; set; }
        public Guid CenterId { get; set; }
        public Guid TypeServiceId { get; set; }
        public decimal Price { get; set; }
        public string ServiceName { get; set; }
        public string CarType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

