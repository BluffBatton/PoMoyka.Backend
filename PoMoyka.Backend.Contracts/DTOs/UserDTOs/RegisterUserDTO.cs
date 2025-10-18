using PoMoyka.Backend.Contracts.DTOs.CarDTOs;
using System.ComponentModel.DataAnnotations;

namespace PoMoyka.Backend.Contracts.DTOs.UserDTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 symbols length")]
        public string Password { get; set; }

        // 2. Добавляем опциональное поле для машины
        public RegisterCarDTO? Car { get; set; }
    }
}
