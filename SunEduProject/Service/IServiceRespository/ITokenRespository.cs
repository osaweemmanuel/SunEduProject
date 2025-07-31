using SunEduProject.Model;

namespace SunEduProject.Service.NewFolder
{
    public interface ITokenRespository
    {
        string GenerateTokenAsync(ApplicationUser user);
    }
}
