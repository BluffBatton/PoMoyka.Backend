using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.Infrastructure.Integration.Reports
{
    public class TopCentersReportDocument : IDocument
    {
        private readonly TopCentersStatisticsDto _data;

        public TopCentersReportDocument(TopCentersStatisticsDto data)
        {
            _data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Header()
                    .Text("Top-selling center")
                    .FontSize(24)
                    .Bold();

                page.Content().Column(col =>
                {
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(40);    // №
                            cols.RelativeColumn(3);      // Name
                            cols.RelativeColumn(1);      // Qty
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("№").Bold();
                            header.Cell().Text("Center").Bold();
                            header.Cell().Text("Qty").Bold();
                        });

                        int index = 1;
                        foreach (var c in _data.Centers)
                        {
                            table.Cell().Text(index++.ToString());
                            table.Cell().Text(c.CenterName);
                            table.Cell().Text(c.Quantity.ToString());
                        }
                    });

                    col.Item().PaddingVertical(20);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1);
                            cols.RelativeColumn(1);
                        });

                        table.Cell().Text("Total sales").Bold();
                        table.Cell().Text(_data.TotalSales.ToString());

                        table.Cell().Text("Total cash").Bold();
                        table.Cell().Text($"{_data.TotalCash} грн");
                    });
                });

                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Generated at ").FontSize(10);
                    txt.Span(DateTime.Now.ToString("yyyy-MM-dd HH:mm")).FontSize(10).Bold();
                });
            });
        }
    }
}
