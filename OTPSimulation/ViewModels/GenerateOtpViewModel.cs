namespace OTPSimulation.ViewModels
{
    public class GenerateOtpViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public GenerateOtpViewModel() { }

        public GenerateOtpViewModel(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
