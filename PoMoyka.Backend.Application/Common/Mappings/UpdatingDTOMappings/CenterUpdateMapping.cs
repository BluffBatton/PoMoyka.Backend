using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.UpdatingDTOMappings
{
    public class CenterUpdateMapping : AutoMapper.Profile
    {
        public CenterUpdateMapping()
        {
            CreateMap<CenterUpdateDto, Center>();
        }
    }
}
