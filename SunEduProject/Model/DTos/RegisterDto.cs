using SunEduProject.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunEduProject.Model.DTos
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]  
        public Gender Gender { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;


    }
}
