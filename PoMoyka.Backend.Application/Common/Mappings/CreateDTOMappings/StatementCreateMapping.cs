using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class StatementCreateMapping : AutoMapper.Profile
    {
        public StatementCreateMapping()
        {
            CreateMap<StatementCreateDto, Statement>();
        }
    }
}
