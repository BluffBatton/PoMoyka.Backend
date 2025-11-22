using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Service;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    //[Authorize]
    public class ServiceController : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ServiceCreateDto dto)
        {
            var command = new CreateServiceCommand { service = dto };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceDto>>> GetAll()
        {
            var query = new GetAllServiceQuery();
            var serviceList = await Mediator.Send(query);
            return serviceList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetById(Guid id)
        {
            var query = new GetServiceByIdQuery(id);
            var serviceDto = await Mediator.Send(query);
            return Ok(serviceDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceUpdateDto dto)
        {
            var command = new UpdateServiceCommand(id, dto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceCommand { Id = id };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
