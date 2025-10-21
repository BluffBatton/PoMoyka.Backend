using PoMoyka.Backend.Contracts.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace PoMoyka.Backend.Contracts.DTOs.CarDTOs
{
    public class RegisterCarDTO
    {
        public string Name { get; set; }

        public string LicensePlate { get; set; }
        public CarType CarType { get; set; }
    }
}
