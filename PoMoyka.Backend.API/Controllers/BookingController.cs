using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Booking;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    //[Authorize]
    public class BookingController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
        {
            var command = new CreateBookingCommand(dto);
            var paymentData = await Mediator.Send(command);
            return Ok(paymentData);
        }
    }
}