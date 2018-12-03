using System;
namespace CardVerifyCore.Models
{
    public class CardValidateRequest
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
    }
}