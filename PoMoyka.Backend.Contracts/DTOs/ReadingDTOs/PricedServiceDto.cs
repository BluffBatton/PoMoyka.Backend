namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class PricedServiceDto
    {
        public Guid CenterServiceId { get; set; }
        public string ServiceName { get; set; }
        public string CarType { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
