using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class CarCreateMapping : AutoMapper.Profile
    {
        public CarCreateMapping()
        {
            CreateMap<CarCreateDto, Car>();
        }
    }
}
