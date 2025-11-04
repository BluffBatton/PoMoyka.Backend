using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class CenterService : BaseEntity
    {
        public decimal Price { get; set; }
        public Guid CenterId { get; set; }
        public required virtual Center Center { get; set; }
        public Guid TypeServiceId { get; set; }
        public required virtual TypeService TypeService { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
