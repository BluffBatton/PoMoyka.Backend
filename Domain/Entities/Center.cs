namespace Domain.Entities
{
    internal class Center
    {
        public Guid CenterID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
