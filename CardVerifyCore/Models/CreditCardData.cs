using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardVerifyCore.Models.Enums
{
    public enum CreditCardType
    {
        Visa,
        MasterCard,
        AmericanExpress,
        JCB,
        Unknown
    }

    internal static class CreditCardData
    {
        public static IDictionary<CreditCardType, CardInfo> AllCardInfos;

        static CreditCardData()
        {
            AllCardInfos = new Dictionary<CreditCardType, CardInfo>();
            InitCardRule();
        }

        private static void InitCardRule()
        {
            #region  Visa card is the card number where expiry year is a leap year.
            #endregion
            var Visa = new CardInfo
            {
                BrandName = "Visa",
                Rules = new List<Rule>
                {
                    new Rule
                    {
                        Lengths =  new List<int> { 16 },
                        Prefixes = new List<string> {"4"},
                        LeapYearCheck = true
                    }
                }
            };

            #region  MasterCard year is a prime number.
            #endregion
            var MasterCard = new CardInfo
            {
                BrandName = "MasterCard",
                Rules = new List<Rule>
                {
                    new Rule
                    {
                        Lengths =  new List<int> { 16 },
                        Prefixes = new List<string> {"5"},
                        PrimeNumberCheck = true
                    }
                }
            };

            #region  AmericanExpress year  has 15 digits
            #endregion
            var AmericanExpress = new CardInfo
            {
                BrandName = "AmericanExpress",
                Rules = new List<Rule>
                {
                    new Rule
                    {
                        Lengths =  new List<int> { 15 },
                        Prefixes = new List<string> {"34", "37"}
                    }
                }
            };

            List<string> _prefixJCB = new List<string>();
            for (int i = 3528; i <= 3589; i++)
            {
                _prefixJCB.Add(i.ToString());
            }

            var JCB = new CardInfo
            {
                BrandName = "JCB",
                Rules = new List<Rule>
                {
                    new Rule
                    {
                        Lengths =  new List<int> { 16 },
                        Prefixes = _prefixJCB
                    }
                }
            };

            AllCardInfos.Add(CreditCardType.Visa, Visa);
            AllCardInfos.Add(CreditCardType.MasterCard, MasterCard);
            AllCardInfos.Add(CreditCardType.AmericanExpress, AmericanExpress);
            AllCardInfos.Add(CreditCardType.JCB, JCB);
        }
    }

    internal class CardInfo
    {
        public CardInfo()
        {
            Rules = new List<Rule>();
            BrandName = "Unknown";
        }

        public List<Rule> Rules { get; set; }
        public string BrandName { get; set; }
    }

    internal class Rule
    {
        public Rule()
        {
            //Default Card Number (Numeric 15 or 16 digits)
            Lengths = new List<int> { 15, 16 };
            Prefixes = new List<string>();
            LeapYearCheck = false;
            PrimeNumberCheck = false;
        }

        public List<int> Lengths { get; set; }
        public List<string> Prefixes { get; set; }
        public bool LeapYearCheck { get; set; }
        public bool PrimeNumberCheck { get; set; }
    }
}