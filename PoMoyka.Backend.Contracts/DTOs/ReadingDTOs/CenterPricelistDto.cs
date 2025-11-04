namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class CenterPricelistDto
    {
        public Guid CenterId { get; set; }
        public string CenterName { get; set; } 
        public string Address { get; set; }
        public List<PricedServiceDto> Services { get; set; }
    }
}