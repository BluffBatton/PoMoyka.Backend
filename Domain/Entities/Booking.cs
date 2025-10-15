using Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace Domain.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime BookedTime { get; set; }
        public Guid UserID { get; set; }
        public required virtual User User { get; set; }
        public Guid CenterServiceID { get; set; } 
        public required virtual CenterService CenterService { get; set; }
        public BookingStatus Status { get; set; }

        public virtual Transaction? Transaction { get; set; }
    }
}