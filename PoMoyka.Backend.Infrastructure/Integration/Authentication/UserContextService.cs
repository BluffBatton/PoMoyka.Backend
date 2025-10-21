using Microsoft.AspNetCore.Http;
using PoMoyka.Backend.Application.Interfaces;
using System.Security.Claims;

namespace PoMoyka.Backend.Infrastructure.Integration.Authentication
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Используется только для извлечения UserId из истёкшего access или refresh токена.
        /// НЕ использовать для авторизации или актуальных access-токенов.
        /// </summary>
        public Guid? GetCurrentUserId()
        {
            var UserIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return UserIdClaim != null ? Guid.Parse(UserIdClaim.Value) : null;
        }

        public ClaimsPrincipal? GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }
}
