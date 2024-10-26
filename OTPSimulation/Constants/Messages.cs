namespace OTPSimulation.Constants
{
    public static class Messages
    {
        public static string EMAIL_OK = "Email containing OTP has been sent successfully.";
        public static string EMAIL_FAIL = "Email address does not exist or sending to the email has failed.";
        public static string EMAIL_INVALID = "Email address is invalid.";
        public static string OTP_IS_OK = "OTP is valid and checked";
        public static string OTP_WRONG_TEN_TRIES = "OTP is wrong after 10 tries";
        public static string TIMEOUT_ONE_MINUTE = "Timeout after 1 min";

        public static string ENTER_OTP = "Please Enter OTP";
        public static string MOCK_EMAIL_INPUT = "Please provide Email";
        public static string IncorrectOtpEnteredMessage(int count)
        {
            return $"Incorrect OTP Entered. You have {count} tries remaining.";
        }

        public static string GeneratedOtpMessageToUser(string otp)
        {
            return $"Your OTP Code is {otp}. The code is valid for 1 minute.";
        }
    }
}
