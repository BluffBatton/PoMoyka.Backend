using PoMoyka.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Domain.Entities
{
    public class UserImage : BaseEntity
    {
        public required string Name { get; set; }
        public required string ImageUrl { get; set; }
        public Guid UserID { get; set; }
        public required virtual User User { get; set; }
    }
}
