using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings;
using PoMoyka.Backend.Application.Services.Center;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    //[Authorize]
    public class CentersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCenter([FromBody] СenterCreateDto dto)
        {
            var command = new CreateCenterCommand(dto);
            var centerId = await Mediator.Send(command);
            return Ok(centerId);
        }

        [HttpPost]
        public async Task<IActionResult> SetCenterServicePrice([FromBody] CenterServicePriceDto dto)
        {
            var command = new SetCenterServicePriceCommand(dto);
            var centerServiceId = await Mediator.Send(command);
            return Ok(centerServiceId);
        }

        [HttpGet]
        public async Task<ActionResult<List<CenterMapDto>>> GetAll()
        {
            var query = new GetAllCentersQuery();
            var centersList = await Mediator.Send(query);
            return Ok(centersList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriceList(Guid id)
        {
            var userId = base.UserId;
            var query = new GetCenterPricelistQuery(id, UserId);
            var dto = await Mediator.Send(query);
            return Ok(dto);
        }
    }
}
