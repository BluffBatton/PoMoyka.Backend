using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings
{
    public class TransactionCreateMapping : AutoMapper.Profile
    {
        public TransactionCreateMapping()
        {
            CreateMap<TransactionCreateDto, Transaction>();
        }
    }
}