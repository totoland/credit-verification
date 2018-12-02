using System;
using System.Globalization;
using System.Linq;

namespace CreditCardVerification.Helpers
{
    internal static class ValidationHelper
    {
        public static bool IsAValidNumber(string number)
        {
            number = number.RemoveWhiteSpace();

            return (number
                .ToCharArray()
                .All(char.IsNumber) &&
                    !string.IsNullOrEmpty(number));
        }

        public static bool IsAValidDateFormat(string date)
        {
            try
            {
                DateTime.ParseExact(date, "MMyyyy", CultureInfo.InvariantCulture);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            

        }

        public static bool IsExpired(string date)
        {
            DateTime cardDate = DateTime.ParseExact( date, "MMyyyy", CultureInfo.InvariantCulture);
            return DateTime.Now > cardDate;
        }

        public static bool IsLeapYear(string date)
        {
            DateTime cardDate = DateTime.ParseExact( date, "MMyyyy", CultureInfo.InvariantCulture);
            return DateTime.IsLeapYear(cardDate.Year);
        }

        public static bool IsYearPrimeNumber(string date)
        {
            DateTime cardDate = DateTime.ParseExact(date, "MMyyyy", CultureInfo.InvariantCulture);

            int number = cardDate.Year;
            if (number == 1) return false;
            if (number == 2) return true;

            var limit = Math.Ceiling(Math.Sqrt(number)); //hoisting the loop limit

            for (int i = 2; i <= limit; ++i)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}