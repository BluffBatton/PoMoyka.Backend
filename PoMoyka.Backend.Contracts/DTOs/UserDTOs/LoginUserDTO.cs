using System.ComponentModel.DataAnnotations;

namespace PoMoyka.Backend.Contracts.DTOs.UserDTOs
{
    public class LoginUserDTO
    {
        [Required (ErrorMessage = "Email is required")]
        [EmailAddress (ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
