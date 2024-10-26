using MimeKit;

namespace OTPSimulation.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MimeMessage email);
    }
}
