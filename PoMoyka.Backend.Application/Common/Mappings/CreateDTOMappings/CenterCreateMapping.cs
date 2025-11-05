using PoMoyka.Backend.Domain.Entities;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class CenterCreateMapping : AutoMapper.Profile
    {
        public CenterCreateMapping() 
        {
            CreateMap<СenterCreateDto, Center>();
        }
    }
}
