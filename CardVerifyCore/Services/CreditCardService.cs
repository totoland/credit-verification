using System;
using System.Linq;
using System.Threading.Tasks;
using CardVerifyCore.Helpers;
using CardVerifyCore.Models.Enums;
using CreditCardVerification.Data.Models;
using CreditCardVerification.Helpers;
using CreditCardVerification.Interfaces.IRepositories;
using CreditCardVerification.Interfaces.IServices;

namespace CreditCardVerification.Interfaces.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _iCreditCardRepository;

        public CreditCardService(ICreditCardRepository _iCreditCardRepository)
        {
            this._iCreditCardRepository = _iCreditCardRepository;
        }

        /***
         * return null if credit card does not exist (DB)
         * return thorw ArgumentException if invalid format
        ***/
        public async Task<CreditCardModel> VerifyCard(string CardNumber, string ExpiryDate)
        {
            CardNumber = CardNumber.RemoveWhiteSpace();
            ExpiryDate = ExpiryDate.RemoveWhiteSpace();

            var inputCardModel = new CreditCardModel
            {
                CardNumber = CardNumber,
                ExpiryDate = ExpiryDate
            };

            CreditCardDetector.CheckValidDomainValidation(inputCardModel);
            CreditCardType CreditCard = CreditCardDetector.GetCreditCardType(inputCardModel);

            #region Check existing on DB 
            #endregion
            var card = await this._iCreditCardRepository.GetById(CardNumber);

            if (card == null)
            {
                return null;
            }

            return new CreditCardModel
            {
                CardNumber = card.CardNumber,
                ExpiryDate = ExpiryDate,
                Type = CreditCard.ToString()
            };
        }

        
    }
}
