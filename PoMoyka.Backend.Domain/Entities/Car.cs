using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Domain.Entities
{
    internal class Car
    {
        public Guid CarID { get; set; }
        public CarType CarType { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
    }
}
