namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class EmployeeCreateDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Guid CenterId { get; set; }
    }
}