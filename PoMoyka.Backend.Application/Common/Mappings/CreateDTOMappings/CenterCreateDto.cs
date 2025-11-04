using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class CenterCreateDto : AutoMapper.Profile
    {
        public CenterCreateDto() 
        {
            CreateMap<CenterCreateDto, Center>();
        }
    }
}
