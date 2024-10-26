using OTPSimulation.DataModels;

namespace OTPSimulation.Interfaces
{
    public interface IEmailOTPModule
    {
        Task GenerateOtpEmailAsync(GenerateOtpDataModel generateOtpDataModel);
        Task SubmitOtpAsync(GenerateOtpDataModel generateOtpDataModel);
    }
}
