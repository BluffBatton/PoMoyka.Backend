using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings
{
    public class UserMapping : AutoMapper.Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
