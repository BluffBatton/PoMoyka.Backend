using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Application.Services.User;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpGet("profile")] 
        public async Task<IActionResult> GetMyProfile()
        {
            var query = new GetUserQuery();

            var userDto = await Mediator.Send(query);

            return Ok(userDto);
        }
    }
}
