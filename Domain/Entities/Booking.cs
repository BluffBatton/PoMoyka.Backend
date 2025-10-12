using Domain.Enums;

namespace Domain.Entities
{
    internal class Booking
    {
        public Guid BookingID { get; set; }
        public Guid UserID { get; set; }
        public List<User> Users { get; set; }
        public Guid CenterServiceID { get; set; } 
        public DateTime booked_time { get; set; }
        public BookingStatus Status { get; set; }
    }
}
