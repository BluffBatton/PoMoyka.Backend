namespace PoMoyka.Backend.Domain.Entities
{
    internal class CenterService
    {
        public Guid CenterServiceID { get; set; }
        public Guid ServiceID { get; set; }
        public List<Service> Services { get; set; }
        public Guid TypeServiceID { get; set; }
        public List<TypeService> TypeServices { get; set; }
    }
}
