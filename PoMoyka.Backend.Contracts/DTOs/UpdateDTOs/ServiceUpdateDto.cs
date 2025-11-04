using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Contracts.DTOs.UpdateDTOs
{
    public class ServiceUpdateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
