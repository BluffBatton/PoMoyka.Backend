using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class TypeServiceCreateDto : AutoMapper.Profile
    {
        public TypeServiceCreateDto() 
        {
            CreateMap<TypeServiceCreateDto, TypeService>();
        }
    }
}
