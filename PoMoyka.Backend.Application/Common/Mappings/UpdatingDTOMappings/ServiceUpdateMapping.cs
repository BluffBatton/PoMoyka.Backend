using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.UpdatingDTOMappings
{
    public class ServiceUpdateMapping : AutoMapper.Profile
    {
        public ServiceUpdateMapping()
        {
            CreateMap<ServiceUpdateDto, Service>();
        }
    }
}
