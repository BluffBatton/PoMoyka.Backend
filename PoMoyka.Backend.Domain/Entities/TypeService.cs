using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Domain.Entities
{
    internal class TypeService
    {
        public Guid TypeServiceID { get; set; }
        public Guid ServiceID { get; set; }
        public List<Service> Services { get; set; }
        public CarType CarType { get; set; }
        public int Price { get; set; }
    }
}
