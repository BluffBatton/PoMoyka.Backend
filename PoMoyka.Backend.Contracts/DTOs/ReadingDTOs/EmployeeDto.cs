namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public Guid? CenterId { get; set; }
        public string? CenterName { get; set; }
    }
}
