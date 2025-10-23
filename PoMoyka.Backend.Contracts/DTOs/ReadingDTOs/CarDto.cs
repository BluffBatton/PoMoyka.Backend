using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CarType CarType { get; set; }
        public string LicensePlate { get; set; }
    }
}
