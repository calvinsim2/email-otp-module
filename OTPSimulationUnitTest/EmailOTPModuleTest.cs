using FakeItEasy;
using OTPSimulation.Constants;
using OTPSimulation.DataModels;
using OTPSimulation.Interfaces;
using OTPSimulation.Services;

namespace OTPSimulationUnitTest
{
    public class EmailOTPModuleTest
    {
        private static readonly IMailService _mailServiceMock = A.Fake<IMailService>();
        private static readonly IConsoleUserInteraction _consoleUserInteractionMock = A.Fake<IConsoleUserInteraction>();

        private EmailOTPModule _emailOTPModule = new EmailOTPModule(_mailServiceMock, _consoleUserInteractionMock);

        [Fact]
        public async Task GenerateOtpEmailAsync_Positive_SendOTPEmailAsyncToBeCalledOnceExactly()
        {
            GenerateOtpDataModel generateOtpDataModelMock = new GenerateOtpDataModel
            {
                UserEmail = "testuser@example.com",
                OTP = "123456"
            };

            await _emailOTPModule.GenerateOtpEmailAsync(generateOtpDataModelMock);

            A.CallTo(() => _mailServiceMock.SendOTPEmailAsync(generateOtpDataModelMock)).WithAnyArguments()
                    .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task SubmitOtpAsync_Positive_CorrectOtpProvided_ShouldReturnWhenOtpMatches()
        {
            // Arrange
            var generateOtpDataModel = new GenerateOtpDataModel { OTP = "123456" };

            A.CallTo(() => _consoleUserInteractionMock.ReadUserInput()).Returns("123456");

            // Act
            await _emailOTPModule.SubmitOtpAsync(generateOtpDataModel);

            // Assert
            Assert.Equal((int)StatusCodes.OK, generateOtpDataModel.StatusCode);
            Assert.Equal(Messages.OTP_IS_OK, generateOtpDataModel.Message);
        }

        [Fact]
        public async Task SubmitOtpAsync_Negative_UserProvideWrongInput_ShouldReturnOtpWrongTenTimes()
        {
            // Arrange
            var generateOtpDataModel = new GenerateOtpDataModel { OTP = "123456" };

            A.CallTo(() => _consoleUserInteractionMock.ReadUserInput()).Returns("789012");

            // Act
            await _emailOTPModule.SubmitOtpAsync(generateOtpDataModel);

            // Assert
            Assert.Equal((int)StatusCodes.BadRequest, generateOtpDataModel.StatusCode);
            Assert.Equal(Messages.OTP_WRONG_TEN_TRIES, generateOtpDataModel.Message);
        }

        [Fact]
        public async Task SubmitOtpAsync_Negative_UserDoNotProvideInput_ReturnsTimeOutOneMinute()
        {
            // Arrange
            var generateOtpDataModel = new GenerateOtpDataModel { OTP = "123456" };

            // returnsLazily allows us to simulate timeout
            A.CallTo(() => _consoleUserInteractionMock.ReadUserInput())
                .ReturnsLazily(() =>
                {
                    Task.Delay(TimeSpan.FromMinutes(1)).Wait();
                    return string.Empty;
                });

            // Act
            await _emailOTPModule.SubmitOtpAsync(generateOtpDataModel);

            // Assert
            Assert.Equal((int)StatusCodes.BadRequest, generateOtpDataModel.StatusCode);
            Assert.Equal(Messages.TIMEOUT_ONE_MINUTE, generateOtpDataModel.Message);
        }
    }
}
