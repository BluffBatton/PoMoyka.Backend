using MediatR;
using Microsoft.AspNetCore.Mvc;
using PoMoyka.Backend.Application.Interfaces;

namespace PoMoyka.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        private IUserContextService _userContextService;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IUserContextService UserContextService =>
            _userContextService ??= HttpContext.RequestServices.GetService<IUserContextService>();

        protected Guid UserId => UserContextService.GetCurrentUserId() ?? Guid.Empty;
    }
}
