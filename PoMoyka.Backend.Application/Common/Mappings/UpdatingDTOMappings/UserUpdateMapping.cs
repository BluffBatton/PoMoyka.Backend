using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.UpdatingDTOMappings
{
    public class UserUpdateMapping : AutoMapper.Profile
    {
        public UserUpdateMapping()
        {
            CreateMap<UserUpdateDto, User>();
        }
    }
}
