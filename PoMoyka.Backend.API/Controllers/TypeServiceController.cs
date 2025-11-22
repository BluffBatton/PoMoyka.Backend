using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.TypeService;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TypeServiceController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TypeServiceCreateDto dto)
        {
            var command = new CreateTypeServiceCommand(dto);
            var typeServiceId = await Mediator.Send(command);
            return Ok(typeServiceId);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<TypeServiceDto>>> GetAll()
        {
            var query = new GetAllTypeServicesQuery();
            var typeServiceList = await Mediator.Send(query);
            return Ok(typeServiceList);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TypeServiceDto>> GetById(Guid id)
        {
            var query = new GetTypeServiceByIdQuery(id);
            var typeServiceDto = await Mediator.Send(query);
            return Ok(typeServiceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TypeServiceUpdateDto dto)
        {
            var command = new UpdateTypeServiceCommand(id, dto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteTypeServiceCommand { Id = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
