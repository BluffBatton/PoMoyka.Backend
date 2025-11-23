using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class CenterDetailedMapping : AutoMapper.Profile
    {
        public CenterDetailedMapping()
        {
            CreateMap<Center, CenterDetailedDto>();
        }
    }
}
