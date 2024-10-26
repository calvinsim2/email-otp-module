namespace OTPSimulation.DataModels
{
    public class GenerateOtpDataModel
    {
        public string UserEmail { get; set; } = string.Empty;
        public string OTP { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public void AssignStatusCodeAndMessage(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
