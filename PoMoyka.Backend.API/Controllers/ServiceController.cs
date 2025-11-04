using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Service;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class ServiceController : BaseController
    {
        [HttpPost]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteServiceCommand { Id = id };
            await Mediator.Send(command);
            return Ok(command);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTypeService([FromBody] TypeServiceCreateDto dto)
        {
            var command = new CreateTypeServiceCommand(dto);
            var typeServiceId = await Mediator.Send(command);
            return Ok(typeServiceId);
        }
    }
}
