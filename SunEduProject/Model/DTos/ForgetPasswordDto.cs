using System.ComponentModel.DataAnnotations;

namespace SunEduProject.Model.DTos
{
    public class ForgetPasswordDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } 
       
    }
}
