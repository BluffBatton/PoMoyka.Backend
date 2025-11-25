using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Statistics;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class StatisticsController : BaseController
    {
        [HttpGet("top-centers")]
        public async Task<IActionResult> GetTopCenters(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var query = new GetTopCentersStatisticsQuery(from, to);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("top-centers/pdf")]
        public async Task<IActionResult> GetTopCentersPdf(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken cancellationToken)
        {
            var report = await Mediator.Send(
                new GenerateTopCentersReportQuery(from, to),
                cancellationToken);
            return File(report.Content, report.ContentType, report.FileName);
        }
    }
}
