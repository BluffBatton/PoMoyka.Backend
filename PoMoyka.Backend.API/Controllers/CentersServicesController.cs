using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Center;
using PoMoyka.Backend.Application.Services.CenterService;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class CentersServicesController : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> Create([FromBody] CenterServicePriceDto dto)
        {
            var command = new SetCenterServicePriceCommand(dto);
            var centerServiceId = await Mediator.Send(command);
            return Ok(centerServiceId);
        }

        [HttpGet("{centerId}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<List<CenterServiceDto>>> GetAll(Guid centerId)
        {
            var query = new GetAllCenterServicesQuery(centerId);
            var services = await Mediator.Send(query);
            return Ok(services);
        }

        [HttpGet("{centerId}")]
        public async Task<ActionResult<CenterPricelistDto>> GetPriceList(Guid centerId)
        {
            var query = new GetCenterPricelistQuery(centerId);
            var dto = await Mediator.Send(query);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CenterServiceUpdateDto dto)
        {
            var command = new UpdateCenterServiceCommand(id, dto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCenterServiceCommand(id);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
