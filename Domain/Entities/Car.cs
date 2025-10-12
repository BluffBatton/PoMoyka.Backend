namespace Domain.Entities
{
    internal class Car
    {
        public Guid CarID { get; set; }
        public Guid TypeID { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
    }
}
