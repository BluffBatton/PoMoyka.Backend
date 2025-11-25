using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class EmployeeMapping : AutoMapper.Profile
    {
        public EmployeeMapping()
        {
            CreateMap<User, EmployeeDto>()
                .ForMember(d => d.CenterId,
                    opt => opt.MapFrom(u => u.Center != null ? u.Center.Id : (Guid?)null))
                .ForMember(d => d.CenterName,
                    opt => opt.MapFrom(u => u.Center != null ? u.Center.Name : null));
        }
    }
}
