using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Common.Mappings.CreateDTOMappings;
using PoMoyka.Backend.Application.Services.Center;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class CentersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCenter([FromBody] CenterCreateDto dto)
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
    }
}
