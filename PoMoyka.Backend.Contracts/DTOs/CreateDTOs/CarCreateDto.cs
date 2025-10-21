using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class CarCreateDto
    {
        public string Name { get; set; }

        public string LicensePlate { get; set; }
        public CarType CarType { get; set; }

        public Guid UserId { get; set; }
    }
}
