using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using OTPSimulation.Interfaces;

namespace OTPSimulation.Common
{
    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;

        public EmailSender(IConfiguration configuration)
        {
            _host = configuration["smtp:host"];
            _port = configuration.GetValue<int>("smtp:port");
            _username = configuration["smtp:username"];
            _password = configuration["smtp:password"];
        }

        public async Task SendEmailAsync(MimeMessage email)
        {
            var smtp = new SmtpClient();
            smtp.Connect(_host, _port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_username, _password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
