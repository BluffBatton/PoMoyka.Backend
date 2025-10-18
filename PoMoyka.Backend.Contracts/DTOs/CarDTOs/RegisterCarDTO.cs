using PoMoyka.Backend.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PoMoyka.Backend.Contracts.DTOs.CarDTOs
{
    public class RegisterCarDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string LicensePlate { get; set; }

        [Required]
        public CarType CarType { get; set; }
    }
}
