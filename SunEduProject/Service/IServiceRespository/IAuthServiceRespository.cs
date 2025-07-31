using SunEduProject.Model.DTos;

namespace SunEduProject.Service.IServiceRespository
{
    public interface IAuthServiceRespository
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<object> ForgetPasswordAsync(ForgetPasswordDto forgetPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}
