

using Azure;
using Microsoft.AspNetCore.Identity;
using SunEduProject.EmailTemplate;
using SunEduProject.Model;
using SunEduProject.Model.DTos;
using SunEduProject.Service.IServiceRespository;
using SunEduProject.Service.NewFolder;
using System.Net;

namespace SunEduProject.Service
{
    public class AuthServiceRespository : IAuthServiceRespository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenRespository tokenRespository;
        private readonly IsendEmailService emailService;

        public AuthServiceRespository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,ITokenRespository tokenRespository,
            IsendEmailService emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenRespository = tokenRespository;
            this.emailService = emailService;
        }

        public async Task<object> ForgetPasswordAsync(ForgetPasswordDto forgetPasswordDto)
        {
           var user = await userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = $"http://localhost:5173/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";

            var emilDto = new SendEmailDto
            {
                To = user.Email,
                Subject = "Password Reset Request",
                Body = GetResetPasswordEmailBody.GetEmailResetBody(resetUrl, user.FirstName, user.LastName)
            };

            await emailService.SendEmailAsync(emilDto);


            return new
            {
                Message = "Password reset link generated",
                user = user.Email,
                token,
                url = resetUrl
            };


        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            try
            {
                var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                if (!result.Succeeded)
                {
                    throw new Exception("Email or Password is invalid.");
                }

                // Create JWT token (no await needed if method is not async)
                var token = tokenRespository.GenerateTokenAsync(user);


                return new LoginResponseDto
                {
                    UserId = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Gender = user.Gender.ToString(),
                    Token = token
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in.", ex);
            }
        }

        

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email, 
                Gender =registerDto.Gender,
                DateOfBirth = DateTime.Now, 
                NormalizedEmail = registerDto.Email.ToUpper(),
                NormalizedUserName = registerDto.Email.ToUpper()
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }

            try
            {
                // Optionally, you can sign in the user after registration
                
                return "User registered successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the user.", ex);
            }

        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
                {
                    throw new Exception("Passwords do not match.");
                }
                var result = await userManager.ResetPasswordAsync(user,resetPasswordDto.Token, resetPasswordDto.NewPassword);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Password reset failed: {errors}");
                }

                await userManager.UpdateSecurityStampAsync(user); //update query
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while resetting the password.", ex);
            }

        }
    }
}



