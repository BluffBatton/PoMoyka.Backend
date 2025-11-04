using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class ServiceMapping : AutoMapper.Profile
    {
        public ServiceMapping()
        {
            CreateMap<Service, ServiceDto>();
        }
    }
}
