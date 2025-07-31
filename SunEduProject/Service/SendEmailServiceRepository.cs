using SunEduProject.Model.DTos;
using SunEduProject.Service.IServiceRespository;
using System.Net.Mail;
using System.Net;

namespace SunEduProject.Service
{
    public class SendEmailServiceRepository : IsendEmailService
    {
        private readonly IConfiguration configuration;

        public SendEmailServiceRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendEmailAsync(SendEmailDto emailDto)
        {
            try
            {
                var smtpClient = new SmtpClient(configuration["Gmail:Smtp"])
                {
                    Port = int.Parse(configuration["Gmail:Port"]),
                    Credentials = new NetworkCredential(
                                       configuration["Gmail:Username"],
                                       configuration["Gmail:Password"]
                                   ),
                    EnableSsl = true

                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(configuration["Gmail:From"]),
                    Subject = emailDto.Subject,
                    Body = emailDto.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(emailDto.To);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while sending the email.", ex);
            }
        }
    }
}
