using System.Text.RegularExpressions;

namespace HotelReservationsApp.Model.Validator
{
    internal static class SpecificChecks
    {
        private const string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        private const string phoneRegex = @"(^[0-9\+]{1,}[0-9\-]{3,15})([;][0-9\+]{1,}[0-9\-]{3,15})*$";

        public static bool ValidateEmail(string email)
        {
            Regex rgx = new Regex(emailRegex);
            return rgx.IsMatch(email);
        }

        public static bool ValidatePhone(string phone)
        {
            Regex rgx = new Regex(phoneRegex);
            return rgx.IsMatch(phone);
        }
    }
}