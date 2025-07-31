using System.ComponentModel.DataAnnotations;

namespace SunEduProject.Model.DTos
{
    public class ForgetPasswordDto
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
       
    }
}
