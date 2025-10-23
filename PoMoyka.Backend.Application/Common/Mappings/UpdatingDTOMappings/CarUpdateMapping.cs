using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.UpdatingDTOMappings
{
    public class CarUpdateMapping : AutoMapper.Profile
    {
        public CarUpdateMapping()
        {
            CreateMap<CarUpdateDto, Car>();
        }
    }
}
