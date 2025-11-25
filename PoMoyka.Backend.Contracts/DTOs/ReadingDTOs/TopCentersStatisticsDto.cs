namespace PoMoyka.Backend.Contracts.DTOs.ReadingDTOs
{
    public class TopCentersStatisticsDto
    {
        public required List<CenterSalesItemDto> Centers { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalCash { get; set; }
    }
}
