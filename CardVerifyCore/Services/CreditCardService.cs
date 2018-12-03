using System;
using System.Threading.Tasks;
using CardVerifyCore.Helpers;
using CardVerifyCore.Models.Enums;
using CreditCardVerification.Data.Models;
using CreditCardVerification.Helpers;
using CreditCardVerification.Interfaces.IRepositories;
using CreditCardVerification.Interfaces.IServices;
using Microsoft.Extensions.Logging;

namespace CreditCardVerification.Interfaces.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _iCreditCardRepository;
        private readonly ILogger _logger;

        public CreditCardService(ICreditCardRepository _iCreditCardRepository,
            ILogger<CreditCardService> _logger)
        {
            this._iCreditCardRepository = _iCreditCardRepository;
            this._logger = _logger;
        }

        /***
         * return null if credit card does not exist (DB)
         * return thorw ArgumentException if invalid format
        ***/
        public async Task<CreditCardModel> VerifyCard(string CardNumber, string ExpiryDate)
        {
            CardNumber = CardNumber.RemoveWhiteSpace();
            ExpiryDate = ExpiryDate.RemoveWhiteSpace();

            var requestModel = new CreditCardModel
            {
                CardNumber = CardNumber,
                ExpiryDate = ExpiryDate
            };

            CreditCardDetector.CheckValidDomainValidation(requestModel);
            CreditCardType CreditCard = CreditCardDetector.GetCreditCardType(requestModel);

            _logger.LogDebug("CardNumber {0} is {1}", CardNumber, CreditCard.ToString());

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
