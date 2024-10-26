using OTPSimulation.Database;
using System.Text.RegularExpressions;

namespace OTPSimulation.Common
{
    public static class EmailChecker
    {
        public static bool IsValidEmailDomain(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                return false;
            }
            // Regex pattern to match emails ending with @dso.org.sg
            string pattern = @"^[^@\s]+@dso\.org\.sg$";
            return Regex.IsMatch(userEmail, pattern, RegexOptions.IgnoreCase);
        }

        public static bool EmailExistInDatabase(string userEmail)
        {
            return ExistingEmailAddress.ValidEmailAddresses.Contains(userEmail);
        }
    }
}
