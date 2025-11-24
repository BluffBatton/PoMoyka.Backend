using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class StatementMappingProfile : AutoMapper.Profile
    {
        public StatementMappingProfile()
        {
            CreateMap<Statement, StatementDto>()
                .ForMember(d => d.FullName,
                    opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName))
                .ForMember(d => d.Email,
                    opt => opt.MapFrom(s => s.User.Email));
        }
    }
}
