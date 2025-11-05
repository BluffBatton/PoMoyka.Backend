using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class TypeServiceCreateMapping : AutoMapper.Profile
    {
        public TypeServiceCreateMapping() 
        {
            CreateMap<TypeServiceCreateDto, TypeService>();
        }
    }
}
