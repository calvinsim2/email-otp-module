using OTPSimulation.DataModels;

namespace OTPSimulation.Interfaces
{
    public interface IMailService
    {
        Task SendOTPEmailAsync(GenerateOtpDataModel generateOtpDataModel);
    }
}
