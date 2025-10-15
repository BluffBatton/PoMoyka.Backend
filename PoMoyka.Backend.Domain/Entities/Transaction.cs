using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public Guid BookingID { get; set; }
        public required virtual Booking Booking { get; set; }
        public virtual Rating ?Rating { get; set; }
    }
}