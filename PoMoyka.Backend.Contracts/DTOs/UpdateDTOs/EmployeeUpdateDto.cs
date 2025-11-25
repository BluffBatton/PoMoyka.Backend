namespace PoMoyka.Backend.Contracts.DTOs.UpdateDTOs
{
    public class EmployeeUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? CenterId { get; set; }
    }
}
