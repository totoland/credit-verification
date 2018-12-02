using System;
using System.Collections.Generic;
using System.Text;

namespace CreditCardVerification.Data.Entities
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public bool? IsActive { get; set; }
        public string ReMark { get; set; }
    }
}
