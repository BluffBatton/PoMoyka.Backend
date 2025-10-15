using PoMoyka.Backend.Domain.Enums;
using PoMoyka.Backend.Domain.Common;

namespace PoMoyka.Backend.Domain.Entities
{
    public class Statement : BaseEntity
    {
        public Guid UserID { get; set; }
        public required virtual User User { get; set; }
        public required string Topic { get; set; }
        public required string Message { get; set; }
        public StatementStatus status { get; set; } = StatementStatus.Unread;
    }
}
