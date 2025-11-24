using PoMoyka.Backend.Contracts.DTOs.Enums;

namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class StatementDto
    {
        public Guid UserId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Topic { get; set; }
        public required string Message { get; set; }
        public StatementStatus Status { get; set; }
    }
}
