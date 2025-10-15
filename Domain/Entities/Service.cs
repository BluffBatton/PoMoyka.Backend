using PoMoyka.Backend.Domain.Common;

namespace Domain.Entities
{
    public class Service : BaseEntity
    {
        public required string Name { get; set; }
        public string ?Description { get; set; }

        public virtual ICollection<TypeService> ?TypeServices { get; set; }
    }
}
