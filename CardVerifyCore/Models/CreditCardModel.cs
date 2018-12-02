using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardVerification.Data.Models
{
    public class CreditCardModel
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string Type { get; set; }
    }
}
