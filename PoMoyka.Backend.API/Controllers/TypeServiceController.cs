using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.TypeService;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    public class TypeServiceController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateTypeService([FromBody] TypeServiceCreateDto dto)
        {
            var command = new CreateTypeServiceCommand(dto);
            var typeServiceId = await Mediator.Send(command);
            return Ok(typeServiceId);
        }
    }
}
