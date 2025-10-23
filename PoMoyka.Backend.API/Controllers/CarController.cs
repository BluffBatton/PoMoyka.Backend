using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Car;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class CarController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetMyCar()
        {
            var query = new GetCarQuery();

            var carDto = await Mediator.Send(query);

            return Ok(carDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMyCar([FromBody] CarUpdateDto carUpdateDto)
        {
            var command = new UpdateCarCommand(carUpdateDto);

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
