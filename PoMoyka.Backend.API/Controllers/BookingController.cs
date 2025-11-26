using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Booking;
using PoMoyka.Backend.Contracts.DTOs.AuthDTOs;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using PoMoyka.Backend.Contracts.DTOs.Enums;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class BookingController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingCreateDto dto)
        {
            var command = new CreateBookingCommand(dto);
            var paymentData = await Mediator.Send(command);
            return Ok(paymentData);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<List<BookingDto>>> GetAll(
            [FromQuery] BookingStatus? status = null,
            [FromQuery] Guid? centerId = null,
            [FromQuery] Guid? userId = null)
        {
            var query = new GetAllBookingsQuery(status, centerId, userId);
            var bookings = await Mediator.Send(query);
            return Ok(bookings);
        }

        [HttpGet]
        [Authorize(Roles = "Client,Admin")]
        public async Task<ActionResult<List<BookingDetailedDto>>> GetMy([FromQuery] BookingStatus? status = null)
        {
            var query = new GetMyBookingsQuery(status);
            var bookings = await Mediator.Send(query);
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetailedDto>> GetById(Guid id)
        {
            var query = new GetBookingByIdQuery(id);
            var booking = await Mediator.Send(query);
            return Ok(booking);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var command = new CancelBookingCommand(id);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var command = new CompleteBookingCommand(id);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("payment-callback")]
        public async Task<IActionResult> PaymentCallback([FromBody] LiqPayCallbackDto dto)
        {
            var command = new ConfirmPaymentCommand(dto);
            await Mediator.Send(command);
            return Ok();
        }
    }
}