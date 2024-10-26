using OTPSimulation.DataModels;
using OTPSimulation.ViewModels;

namespace OTPSimulation.Mapper
{
    public static class ViewModelAndDataModelMapping
    {
        public static void MapDataModelStatusCodeAndMessageToViewModel(GenerateOtpViewModel generateOtpViewModel,
                                                                       GenerateOtpDataModel generateOtpDataModel)
        {
            generateOtpViewModel.StatusCode = generateOtpDataModel.StatusCode;
            generateOtpViewModel.Message = generateOtpDataModel.Message;
        }
    }
}
