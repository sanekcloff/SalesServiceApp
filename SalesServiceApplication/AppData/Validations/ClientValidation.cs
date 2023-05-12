using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validations
{
    public static class ClientValidation
    {
        public static bool IsValidPhoneNumber(string phone)
        {
            return !string.IsNullOrEmpty(phone) ? IsDigits(phone) && (phone[0] == '8' && phone.Length==11) : false;
        }
        private static bool IsDigits(string phone)
        {
            foreach (var digit in phone)
            {
                if (digit < '0' || digit > '9')
                    return false;
            }
            return true;
        }
        public static bool IsValidEmail(string email)
        {
            var result=false;
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    MailAddress m = new MailAddress(email);
                    result= true;
                }
                catch (FormatException)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
