using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.UpdateDTOs
{
    public class CarUpdateDto
    {
        public string ?Name { get; set; }
        public string ?LicensePlate { get; set; }
        public CarType ?CarType { get; set; } 
    }
}
