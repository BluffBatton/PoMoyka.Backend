using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class ServiceCreateMapping : AutoMapper.Profile
    {
        ServiceCreateMapping()
        {
            CreateMap<ServiceCreateDto, ServiceCreateDto>();
        }
    }
}
