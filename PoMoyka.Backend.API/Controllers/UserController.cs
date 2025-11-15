using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Services.User;
using PoMoyka.Backend.Application.Services.UserImage;
using PoMoyka.Backend.Contracts.DTOs.ReadingDTOs;
using PoMoyka.Backend.Contracts.DTOs.UpdateDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpGet] 
        public async Task<IActionResult> GetMyProfile()
        {
            var query = new GetUserQuery();

            var userDto = await Mediator.Send(query);

            return Ok(userDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserUpdateDto updateDto)
        {
            var command = new UpdateUserCommand(updateDto);

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> UploadImage([FromForm] IFormFile image)
        {
            var command = new UploadUserImageCommand
            {
                Image = image
            };

            var imageId = await Mediator.Send(command);
            return Ok(imageId);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteImage()
        {
            var command = new DeleteUserImageCommand();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetUserImageUrl()
        {
            var url = await Mediator.Send(new GetUserImageQuery() );
            return Ok(url);
        }
    }
}
