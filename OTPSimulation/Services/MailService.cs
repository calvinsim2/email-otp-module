using MimeKit;
using OTPSimulation.Constants;
using OTPSimulation.DataModels;
using OTPSimulation.Interfaces;

namespace OTPSimulation.Services
{
    public class MailService : IMailService
    {
        public readonly IEmailSender _emailSender;
        public MailService(IEmailSender emailSender) 
        {
            _emailSender = emailSender;
        }

        public async Task SendOTPEmailAsync(GenerateOtpDataModel generateOtpDataModel)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(ConstantValues.EMAIL_SENDER);
            email.To.Add(MailboxAddress.Parse(generateOtpDataModel.UserEmail));
            email.Subject = ConstantValues.EMAIL_SUBJECT;

            var builder = new BodyBuilder();

            builder.HtmlBody = Messages.GeneratedOtpMessageToUser(generateOtpDataModel.OTP);
            email.Body = builder.ToMessageBody();

            await _emailSender.SendEmailAsync(email);

        }
    }
}
