using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.UpdateDTOMappings
{
    public class EmployeeUpdateMapping : AutoMapper.Profile
    {
        public EmployeeUpdateMapping()
        {
            CreateMap<EmployeeUpdateDto, User>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
