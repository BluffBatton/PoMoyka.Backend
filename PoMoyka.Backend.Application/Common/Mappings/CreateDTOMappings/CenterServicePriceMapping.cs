using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class CenterServicePriceMapping : AutoMapper.Profile
    {
        public CenterServicePriceMapping() 
        {
            CreateMap<CenterServicePriceDto, CenterService>();
        }
    }
}
