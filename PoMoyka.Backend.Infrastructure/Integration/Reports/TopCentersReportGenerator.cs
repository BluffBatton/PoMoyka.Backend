using PoMoyka.Backend.Application.Interfaces;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using QuestPDF.Fluent;

namespace PoMoyka.Backend.Infrastructure.Integration.Reports
{
    public class TopCentersReportGenerator : ITopCentersReportGenerator
    {
        public byte[] Generate(TopCentersStatisticsDto data)
        {
            var doc = new TopCentersReportDocument(data); 
            return doc.GeneratePdf();
        }
    }
}
