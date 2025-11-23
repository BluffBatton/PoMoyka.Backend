using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Contracts.DTOs.AuthDTOs;
using PoMoyka.Backend.Application.Services.Payment;

namespace PoMoyka.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> LiqPayCallback([FromForm] LiqPayCallbackDto dto)
        {
            var command = new ConfirmPaymentCommand(dto.Data, dto.Signature);

            await _mediator.Send(command);

            return Ok();
        }
    }
}