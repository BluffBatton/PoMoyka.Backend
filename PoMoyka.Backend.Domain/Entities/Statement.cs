using PoMoyka.Backend.Domain.Enums;

namespace PoMoyka.Backend.Domain.Entities
{
    internal class Statement
    {
        public Guid StatementID { get; set; }
        public Guid UserID { get; set; }
        public List<User> Users { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public StatementStatus status { get; set; } = StatementStatus.Unread;
    }
}
