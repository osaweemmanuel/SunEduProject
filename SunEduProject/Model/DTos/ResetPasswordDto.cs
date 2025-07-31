using System.ComponentModel.DataAnnotations;

namespace SunEduProject.Model.DTos
{
    public class ResetPasswordDto
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
      
        public string Token { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Required, DataType(DataType.Password), Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
