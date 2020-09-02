using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelReservationsApp.Model.Validator
{
    static class SpecificCheckercs
    {
        const string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        const string phoneRegex = @"(^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*)([;]( )*([+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*))*";

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
