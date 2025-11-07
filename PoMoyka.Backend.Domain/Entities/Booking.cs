using PoMoyka.Backend.Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime BookedTime { get; set; }
        public BookingStatus Status { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid CenterServiceId { get; set; } 
        public virtual CenterService CenterService { get; set; }

        public virtual Transaction? Transaction { get; set; }
    }
}
