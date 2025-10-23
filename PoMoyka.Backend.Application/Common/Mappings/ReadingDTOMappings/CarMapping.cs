using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class CarMapping : AutoMapper.Profile
    {
        public CarMapping()
        {
            CreateMap<Car, CarDto>();
        }
    }
}
