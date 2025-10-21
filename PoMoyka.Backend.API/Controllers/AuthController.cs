using MediatR;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Contracts.DTOs.UserDTOs;
using PoMoyka.Backend.Application.Services.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using PoMoyka.Backend.API.Controllers;
using PoMoyka.Backend.Contracts.DTOs.AuthDTOs;

namespace PoMoyka.Backend.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
        //{
        //    // 3. Создаем Команду (RegisterCommand) и "заворачиваем" в нее DTO
        //    var command = new RegisterUserCommand(dto);

        //    // 4. Отправляем команду в MediatR.
        //    // MediatR сам найдет нужный RegisterCommandHandler и выполнит его.
        //    var token = await _mediator.Send(command);

        //    // 5. Возвращаем клиенту токен
        //    return Ok(new { Token = token });
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        //{
        //    var command = new LoginCommand(dto);
        //    var token = await _mediator.Send(command);
        //    return Ok(new { Token = token });
        //}



        public async Task<ActionResult<Guid>> Register([FromBody] UserCreateDto user)
        {
            var command = new RegisterUserCommand { User = user };
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }

        //[HttpPost]
        //public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        //{
        //    var command = new LoginCommand { LoginDto = loginDto };
        //    var response = await Mediator.Send(command);
        //    return Ok(response);
        //}

        //[HttpPost]
        //public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        //{
        //    var command = new RefreshTokenCommand { RefreshTokenDto = refreshTokenDto };
        //    var response = await Mediator.Send(command);
        //    return Ok(response);
        //}
    }
}