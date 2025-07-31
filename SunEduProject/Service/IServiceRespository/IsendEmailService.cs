using SunEduProject.Model.DTos;

namespace SunEduProject.Service.IServiceRespository
{
    public interface IsendEmailService
    {
         Task SendEmailAsync(SendEmailDto emailDto);
    }
}
