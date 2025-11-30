namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class CenterDetailedDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<PricedServiceDto> Services { get; set; } = new List<PricedServiceDto>();
        
        // Rating info
        public double? AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }
}

