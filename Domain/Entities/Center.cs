using PoMoyka.Backend.Domain.Common;

namespace Domain.Entities
{
    public class Center : BaseEntity
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid UserID { get; set; }
        public virtual User ?User { get; set; }
        public virtual ICollection<CenterService> ?CenterServices { get; set; }
    }
}
