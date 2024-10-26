using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using OTPSimulation.Constants;
using OTPSimulation.Interfaces;

namespace OTPSimulation.Common
{
    public class EmailSender : IEmailSender
    {
        private readonly string _username;
        private readonly string _password;

        public EmailSender(IConfiguration configuration)
        {
            _username = configuration["smtp:username"];
            _password = configuration["smtp:password"];
        }

        public async Task SendEmailAsync(MimeMessage email)
        {
            var smtp = new SmtpClient();
            smtp.Connect(ConstantValues.EMAIL_HOST, ConstantValues.EMAIL_PORT, SecureSocketOptions.StartTls);
            smtp.Authenticate(_username, _password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
