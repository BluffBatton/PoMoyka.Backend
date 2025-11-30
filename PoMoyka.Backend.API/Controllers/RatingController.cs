using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.Rating;
using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class RatingController : BaseController
    {
        /// <summary>
        /// Добавить рейтинг к транзакции (только для Client)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create([FromBody] RatingCreateDto dto)
        {
            var command = new CreateRatingCommand(dto);
            var ratingId = await Mediator.Send(command);
            return Ok(new { ratingId });
        }
    }
}

