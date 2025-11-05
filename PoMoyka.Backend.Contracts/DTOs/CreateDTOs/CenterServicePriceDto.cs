namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class CenterServicePriceDto
    {
        public decimal Price { get; set; }

        public Guid CenterId { get; set; }

        public Guid TypeServiceId { get; set; }
    }
}
