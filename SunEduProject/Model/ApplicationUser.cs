using Microsoft.AspNetCore.Identity;

using SunEduProject.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunEduProject.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; }= new List<TodoItem>();
    }
}
