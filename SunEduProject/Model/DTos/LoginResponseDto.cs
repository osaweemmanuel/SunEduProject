namespace SunEduProject.Model.DTos
{
    public class LoginResponseDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}
