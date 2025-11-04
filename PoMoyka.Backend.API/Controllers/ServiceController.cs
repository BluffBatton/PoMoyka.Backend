using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Service;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class ServiceController : BaseController
    {
        public async Task<IActionResult> Create([FromBody] ServiceCreateDto dto)
        {
            var command = new CreateServiceCommand { service = dto };
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
