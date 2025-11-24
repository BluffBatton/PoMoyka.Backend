using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class StatementCreateDto
    {
        public required string Topic { get; set; }
        public required string Message { get; set; }
        public StatementStatus Status { get; set; } = StatementStatus.Unread;
    }
}