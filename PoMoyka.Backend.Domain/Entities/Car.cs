using PoMoyka.Backend.Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class Car : BaseEntity
    {
        public CarType CarType { get; set; } 
        public required string Name { get; set; }
        public required string LicensePlate { get; set; }
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
