using System.ComponentModel.DataAnnotations;

namespace SunEduProject.Model.DTos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        
    }
}
