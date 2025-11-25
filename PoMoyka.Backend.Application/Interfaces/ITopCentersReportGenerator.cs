using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Application.Interfaces
{
    public interface ITopCentersReportGenerator
    {
        byte[] Generate(TopCentersStatisticsDto data);
    }
}
