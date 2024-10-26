using OTPSimulation.Constants;
using OTPSimulation.DataModels;
using OTPSimulation.Interfaces;
using System.Security.Cryptography;

namespace OTPSimulation.Services
{
    public class EmailOTPModule : IEmailOTPModule
    {
        public readonly IMailService _mailService;
        public readonly IConsoleUserInteraction _consoleUserInteraction;
        public EmailOTPModule(IMailService mailService, IConsoleUserInteraction consoleUserInteraction)
        {
            _mailService = mailService;
            _consoleUserInteraction = consoleUserInteraction;
        }
        public async Task GenerateOtpEmailAsync(GenerateOtpDataModel generateOtpDataModel)
        {
            //implement 

            generateOtpDataModel.OTP = GenerateOTP();
            await _mailService.SendOTPEmailAsync(generateOtpDataModel);

        }

        public async Task SubmitOtpAsync(GenerateOtpDataModel generateOtpDataModel)
        {
            var inputTask = Task.Run(() =>
            {
                CheckIfOtpMatches(generateOtpDataModel);

            });

            var timeoutTask = Task.Delay(TimeSpan.FromMinutes(1));

            var completedTask = await Task.WhenAny(inputTask, timeoutTask);

            bool userInputBeforeTimeOut = completedTask == inputTask;

            if (userInputBeforeTimeOut)
            {
                return;
            }
            else
            {
                generateOtpDataModel.AssignStatusCodeAndMessage((int)StatusCodes.BadRequest, Messages.TIMEOUT_ONE_MINUTE);
            }
        }

        private void CheckIfOtpMatches(GenerateOtpDataModel generateOtpDataModel)
        {
            int count = ConstantValues.OTP_MAX_ATTEMPTS;
            while (count > 0)
            {
                _consoleUserInteraction.WriteLine(Messages.ENTER_OTP);
                string input = _consoleUserInteraction.ReadUserInput();

                if (input != generateOtpDataModel.OTP)
                {
                    count--;
                    _consoleUserInteraction.WriteLine(Messages.IncorrectOtpEnteredMessage(count));
                }
                else
                {
                    generateOtpDataModel.AssignStatusCodeAndMessage((int)StatusCodes.OK, Messages.OTP_IS_OK);
                    return;
                }

            }

            generateOtpDataModel.AssignStatusCodeAndMessage((int)StatusCodes.BadRequest, Messages.OTP_WRONG_TEN_TRIES);

        }
        private string GenerateOTP()
        {
            byte[] byteArray = new byte[4];
            RandomNumberGenerator.Fill(byteArray);
            int randomValue = BitConverter.ToInt32(byteArray, 0) & 0x7FFFFFFF;
            return (randomValue % 1000000).ToString("D6");
        }

    }
}
