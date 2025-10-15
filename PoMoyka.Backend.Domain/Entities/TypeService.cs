using PoMoyka.Backend.Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class TypeService : BaseEntity
    {
        public Guid ServiceID { get; set; }
        public required virtual Service Service { get; set; }
        public CarType CarType { get; set; }
        public virtual ICollection<CenterService>? CenterServices { get; set; }
    }
}
