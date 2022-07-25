using System;
using System.Text.RegularExpressions;

namespace Hubtel.eCommerce.Cart.Api.Utility
{
    public static class PhoneNumberUtility
    {
        public static bool IsValidMsisdn12Digits(string phoneNumber)
        {
            phoneNumber = phoneNumber.Trim();
            //check for valid phone number
            if (phoneNumber == null ||
                phoneNumber.Trim() == "" ||
                !Regex.IsMatch(phoneNumber, @"^\d+$"))
            {
                return false;
            }

            //init variable to hold the formatted phone number

            if (phoneNumber.StartsWith("+233") && phoneNumber.Length == 13)
            {
                phoneNumber = phoneNumber.Substring(1);
            }
            if (phoneNumber.StartsWith("00233") && phoneNumber.Length == 14)
            {
                phoneNumber = phoneNumber.Substring(2);
            }
            if (phoneNumber.StartsWith("0") && phoneNumber.Length == 10)
            {
                phoneNumber = "233" + phoneNumber.Substring(1);
            }
            if (phoneNumber.Length == 9)
            {
                phoneNumber = "233" + phoneNumber;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }
            if (phoneNumber.Length != 12)
            {
                return false;
            }
            return true;
        }

        public static string FormatNumber12Digits(string phoneNumber)
        {
            phoneNumber = phoneNumber.Trim();
            //check for valid phone number
            if (phoneNumber == null ||
                phoneNumber.Trim() == "" ||
                !Regex.IsMatch(phoneNumber, @"^\d+$"))
            {
                throw new InvalidOperationException("Invalid Number");
            }

            //init variable to hold the formatted phone number

            if (phoneNumber.StartsWith("+233") && phoneNumber.Length == 13)
            {
                phoneNumber = phoneNumber.Substring(1);
            }
            if (phoneNumber.StartsWith("00233") && phoneNumber.Length == 14)
            {
                phoneNumber = phoneNumber.Substring(2);
            }
            if (phoneNumber.StartsWith("0") && phoneNumber.Length == 10)
            {
                phoneNumber = "233" + phoneNumber.Substring(1);
            }
            if (phoneNumber.Length == 9)
            {
                phoneNumber = "233" + phoneNumber;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new InvalidOperationException("Invalid Number");
            }
            if (phoneNumber.Length != 12)
            {
                throw new InvalidOperationException("Invalid Number");
            }
            return phoneNumber;
        }

    }
}
