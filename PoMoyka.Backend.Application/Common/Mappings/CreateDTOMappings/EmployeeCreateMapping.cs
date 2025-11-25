using PoMoyka.Backend.Contracts.DTOs.Enums;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class EmployeeCreateMapping : AutoMapper.Profile
    {
        public EmployeeCreateMapping()
        {
            CreateMap<EmployeeCreateDto, User>()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(u => u.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(d => d.LastName))
                .ForMember(u => u.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(u => u.PasswordHash, opt => opt.Ignore())
                .ForMember(u => u.Role, opt => opt.MapFrom(_ => Role.Employee));
        }
    }
}