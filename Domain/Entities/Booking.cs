using Domain.Enums;

namespace Domain.Entities
{
    internal class Booking
    {
        public Guid BookingID { get; set; }
        public User User { get; set; }
        public Guid CenterServiceID { get; set; } 
        public DateTime booked_time { get; set; }
        BookingStatus Status { get; set; }
    }
}
