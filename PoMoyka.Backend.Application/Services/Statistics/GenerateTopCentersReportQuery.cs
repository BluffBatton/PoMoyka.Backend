using MediatR;
using PoMoyka.Backend.Application.Common.Mappings.ReadingDTOMappings;
using PoMoyka.Backend.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Application.Services.Statistics
{
    public class GenerateTopCentersReportQuery : IRequest<FileReportDto>
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public GenerateTopCentersReportQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }

    public class GenerateTopCentersReportQueryHandler
    : IRequestHandler<GenerateTopCentersReportQuery, FileReportDto>
    {
        private readonly IMediator _mediator;
        private readonly ITopCentersReportGenerator _reportGenerator;

        public GenerateTopCentersReportQueryHandler(
            IMediator mediator,
            ITopCentersReportGenerator reportGenerator)
        {
            _mediator = mediator;
            _reportGenerator = reportGenerator;
        }

        public async Task<FileReportDto> Handle(
            GenerateTopCentersReportQuery request,
            CancellationToken cancellationToken)
        {
            var stats = await _mediator.Send(
                new GetTopCentersStatisticsQuery(request.From, request.To),
                cancellationToken);

            var bytes = _reportGenerator.Generate(stats);

            return new FileReportDto
            {
                FileName = $"top-centers_{request.From:yyyyMMdd}_{request.To:yyyyMMdd}.pdf",
                ContentType = "application/pdf",
                Content = bytes
            };
        }
    }

}
