using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class CenterServicePriceDto : AutoMapper.Profile
    {
        CenterServicePriceDto() 
        {
            CreateMap<CenterServicePriceDto, CenterService>();
        }
    }
}
