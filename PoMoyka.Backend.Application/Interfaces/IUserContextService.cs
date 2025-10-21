using System.Security.Claims;

namespace PoMoyka.Backend.Application.Interfaces
{
    public interface IUserContextService
    {
        Guid? GetCurrentUserId();
        ClaimsPrincipal? GetCurrentUser();
        bool IsAuthenticated();
    }
}
