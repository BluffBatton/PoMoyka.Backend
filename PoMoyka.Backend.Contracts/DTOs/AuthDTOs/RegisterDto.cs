using PoMoyka.Backend.Contracts.DTOs.CreateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoMoyka.Backend.Contracts.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        public UserCreateDto User { get; set; }
        public CarCreateDto Car { get; set; }
    }
}
