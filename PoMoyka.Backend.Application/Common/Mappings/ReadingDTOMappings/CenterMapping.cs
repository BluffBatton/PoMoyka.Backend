using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class CenterMapping : AutoMapper.Profile
    {
        public CenterMapping()
        {
            CreateMap<Center, CenterMapDto>();
        }
    }
}
