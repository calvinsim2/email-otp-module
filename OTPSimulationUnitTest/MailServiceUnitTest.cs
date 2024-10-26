using FakeItEasy;
using MimeKit;
using OTPSimulation.Constants;
using OTPSimulation.DataModels;
using OTPSimulation.Interfaces;
using OTPSimulation.Services;

namespace OTPSimulationUnitTest
{
    public class MailServiceUnitTest
    {
        private static readonly IEmailSender _emailSenderMock = A.Fake<IEmailSender>();

        private MailService _mailService = new MailService(_emailSenderMock);

        [Fact]
        public async Task SendOTPEmailAsync_Positive_SendToUserWithCorrectEmail()
        {
            GenerateOtpDataModel generateOtpDataModelMock = new GenerateOtpDataModel
            {
                UserEmail = "testuser@example.com",
                OTP = "123456"
            };

            await _mailService.SendOTPEmailAsync(generateOtpDataModelMock);

            A.CallTo(() => _emailSenderMock.SendEmailAsync(A<MimeMessage>.That.Matches(email =>
                    email.To[0].ToString() == generateOtpDataModelMock.UserEmail &&
                    email.Subject == ConstantValues.EMAIL_SUBJECT &&
                    ((TextPart)email.Body).Text.Contains(generateOtpDataModelMock.OTP)
                ))).MustHaveHappenedOnceExactly();
        }

    }
}