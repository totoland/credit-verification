using CardVerifyCore.Models.Enums;
using CreditCardVerification.Data.Models;
using CreditCardVerification.Helpers;
using System;
using System.Linq;

namespace CardVerifyCore.Helpers
{
    internal static class CreditCardDetector 
    {
        public static CreditCardType GetCreditCardType(CreditCardModel creditCard)
        {
            CreditCardType Brand = CreditCardType.Unknown;

            foreach (var brandData in CreditCardData.AllCardInfos)
            {
                var cardInfo = brandData.Value;

                foreach (var rule in cardInfo.Rules)
                {
                    if (rule.Lengths.Any(c => c == creditCard.CardNumber.Length) &&
                        rule.Prefixes.Any(c => creditCard.CardNumber.StartsWith(c)))
                    {
                        if (rule.LeapYearCheck)
                        {
                            //TODO LeapYear
                            CheckLeapYear(creditCard.ExpiryDate);
                        }

                        if (rule.PrimeNumberCheck)
                        {
                            //TODO PrimeNumberCheck
                            CheckPrimeNumber(creditCard.ExpiryDate);

                        }

                        Brand = brandData.Key;
                        return Brand;
                    }
                }
            }


            return Brand;
        }

        public static void CheckValidDomainValidation(CreditCardModel creditCard)
        {
            //Card Number (Numeric 15 or 16 digits) 
            //Expiry date (MMYYYY)

            if (!ValidationHelper.IsAValidNumber(creditCard.CardNumber))
                throw new ArgumentException("Invalid number, just numbers is accepted on the string.");

            if (!ValidationHelper.IsAValidDateFormat(creditCard.ExpiryDate))
                throw new ArgumentException("Invalid expiry date.");

            if (ValidationHelper.IsExpired(creditCard.ExpiryDate))
                throw new ArgumentException("Credit card has expired.");
        }

        private static void CheckLeapYear(string ExpiryDate)
        {
            if (!ValidationHelper.IsLeapYear(ExpiryDate))
                throw new ArgumentException("Year is not a leap year.");
        }

        private static void CheckPrimeNumber(string ExpiryDate)
        {
            if (!ValidationHelper.IsYearPrimeNumber(ExpiryDate))
                throw new ArgumentException("Year is not a prime number.");
        }
    }
}