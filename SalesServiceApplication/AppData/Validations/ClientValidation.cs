using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppData.Validations
{
    public static class ClientValidation
    {
        public static bool IsValidUserData(string data) => !string.IsNullOrEmpty(data) ? IsCorrectUserData(data) : false;

        public static bool IsValidName(string name) => !string.IsNullOrEmpty(name) ? IsLetter(name) : false;

        public static bool IsValidPhoneNumber(string phone) => !string.IsNullOrEmpty(phone) ? IsDigits(phone) && phone.Length==10 : false;
        public static bool IsValidOrganization(string organization) => !string.IsNullOrEmpty(organization) ? IsCorrectOrgnization(organization)  : false;

        static string userDataPattern = @"^[a-zA-Z0-9]+$";
        static string organizationPattern = @"^[а-яА-Я0-9,.()-:;""'`№]+$";
        private static bool IsCorrectUserData(string data) => Regex.IsMatch(data, userDataPattern);
        private static bool IsCorrectOrgnization(string organization) => Regex.IsMatch(organization, organizationPattern);

        private static bool IsDigits(string phone)
        {
            foreach (var digit in phone)
            {
                if (!char.IsDigit(digit))
                    return false;
            }
            return true;
        }
        private static bool IsLetter(string name)
        {
            foreach (var letter in name)
            {
                if (!char.IsLetter(letter))
                    return false;
            }
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
