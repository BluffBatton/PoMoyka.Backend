using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Center;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    public class CentersController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CenterDto>>> GetAll()
        {
            var query = new GetAllCentersQuery();
            var centersList = await Mediator.Send(query);
            return Ok(centersList);
        }

        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> Create([FromBody] СenterCreateDto dto)
        {
            var command = new CreateCenterCommand(dto);
            var centerId = await Mediator.Send(command);
            return Ok(centerId);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CenterDetailedDto>> GetById(Guid id)
        {
            var query = new GetCenterByIdQuery(id);
            var center = await Mediator.Send(query);
            return Ok(center);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CenterUpdateDto dto)
        {
            var command = new UpdateCenterCommand(id, dto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCenterCommand(id);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
