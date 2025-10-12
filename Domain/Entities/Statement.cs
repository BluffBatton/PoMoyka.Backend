using Domain.Enums;

namespace Domain.Entities
{
    internal class Statement
    {
        public Guid StatementID { get; set; }
        public Guid UserID { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        StatementStatus status { get; set; } = StatementStatus.Unread;
    }
}
