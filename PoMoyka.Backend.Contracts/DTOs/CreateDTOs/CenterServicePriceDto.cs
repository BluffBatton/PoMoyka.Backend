using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Contracts.DTOs.CreateDTOs
{
    public class CenterServicePriceDto
    {
        public Guid CenterId { get; set; }

        public Guid TypeServiceId { get; set; }

        public int Price { get; set; }
    }
}
