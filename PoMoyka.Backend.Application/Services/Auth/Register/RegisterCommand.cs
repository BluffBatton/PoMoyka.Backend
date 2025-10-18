using MediatR;
using PoMoyka.Backend.Contracts.DTOs.UserDTOs;

namespace PoMoyka.Backend.Application.Services.Auth.Register
{
    public class RegisterCommand : IRequest<string>
    {
        public RegisterUserDTO UserData { get; }

        public RegisterCommand(RegisterUserDTO userData)
        {
            UserData = userData;
        }
    }
}
