using MediatR;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Contracts.DTOs.UserDTOs;
using PoMoyka.Backend.Application.Services.Auth.Register;
using PoMoyka.Backend.Application.Services.Auth.Login;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator; // Инжектим ТОЛЬКО MediatR

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
    {
        // 3. Создаем Команду (RegisterCommand) и "заворачиваем" в нее DTO
        var command = new RegisterCommand(dto);

        // 4. Отправляем команду в MediatR.
        // MediatR сам найдет нужный RegisterCommandHandler и выполнит его.
        var token = await _mediator.Send(command);

        // 5. Возвращаем клиенту токен
        return Ok(new { Token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
    {
        var command = new LoginCommand(dto);
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }
}