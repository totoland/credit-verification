using CardVerifyCore;
using CardVerifyCore.Controllers;
using CardVerifyCore.Models;
using CardVerifyCore.Models.Enums;
using CreditCardVerification.Data.Entities;
using CreditCardVerification.Data.Models;
using CreditCardVerification.Interfaces.IRepositories;
using CreditCardVerification.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CardVerifyTest.Controllers.Test
{
    public class CardValidationControllerTest
    {
        private readonly CardValidationController _cardValidationController;
        private readonly string[] ValidVisas = { "4397435601344857" };
        private readonly string[] ValidMasterCards = { "5290389773644435" };
        private readonly string[] ValidAmexs = { "349309553334447", "379309553334447" };
        private readonly string[] ValidJCBs = { "3528095533344470", "3589095533344470" };
        private readonly string[] UnknowCards = { "1111111111111111" };
        private readonly string[] InvalidDomainCards = { "111","43243dsfdsfs","whyalwaysme" };

        private const string _expiryLeapYear = "022028";
        private const string _expiryPrimeYear = "022027";

        #region Setup Mock
        public CardValidationControllerTest()
        {

            var loggerController =  Mock.Of<ILogger<CardValidationController>>();
            var loggerService = Mock.Of<ILogger<CreditCardService>>();

            IOptions<AppSettings> _appSettings = Mock.Of<IOptions<AppSettings>>();
            CreditCardContext _dbContext = new CreditCardContext(_appSettings);
            ICreditCardRepository _iCreditCardRepository;

            var mockRepo = new Mock<ICreditCardRepository>();

            _iCreditCardRepository = mockRepo.Object;

            List<string> validCards = new List<string>();
            validCards.AddRange(ValidVisas);
            validCards.AddRange(ValidMasterCards);
            validCards.AddRange(ValidAmexs);
            validCards.AddRange(ValidJCBs);
            validCards.AddRange(UnknowCards);

            foreach (string card in validCards)
            {
                mockRepo.Setup(r => r.GetById(card)).ReturnsAsync(new CreditCard
                {
                    CardNumber = card,
                    IsActive = true
                });
            }

            var _iCreditCardService = new CreditCardService(_iCreditCardRepository, loggerService);

            _cardValidationController = new CardValidationController(_iCreditCardService, loggerController);
        }
        #endregion

        [Fact]
        public async Task Test_Visa_Valid()
        {
            //Test with 4397435601344857
            var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = ValidVisas[0],
                        ExpiryDate = _expiryLeapYear
                    }
                );

            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            ServerResponse resp = okResult.Value as ServerResponse;

            //expect is Visa
            Assert.True(CreditCardType.Visa.ToString() == ((CreditCardModel)resp.Result).Type, resp.RespDesc);

        }

        [Fact]
        public async Task Test_MasterCard_Valid()
        {
            //Test with 5290389773644435 with prime year 022027
            var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = ValidMasterCards[0],
                        ExpiryDate = _expiryPrimeYear
                    }
                );

            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            ServerResponse resp = okResult.Value as ServerResponse;

            //expect is MasterCard
            Assert.True(CreditCardType.MasterCard.ToString() == ((CreditCardModel)resp.Result).Type, resp.RespDesc);
        }

        [Fact]
        public async Task Test_Amex_Valid()
        {
            //Test with "349309553334447", "379309553334447" with year 022029
            foreach (string validCard in ValidAmexs)
            {
                var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = validCard,
                        ExpiryDate = "022029"
                    }
                );

                var okResult = actionResult as OkObjectResult;

                // Assert
                Assert.Equal(200, okResult.StatusCode);
                ServerResponse resp = okResult.Value as ServerResponse;

                //expect is Amex
                Assert.True(CreditCardType.AmericanExpress.ToString() == ((CreditCardModel)resp.Result).Type, resp.RespDesc);
            }
        }

        [Fact]
        public async Task Test_JCB_Valid()
        {
            //Test with "352809553334447", "358909553334447" with year 022029
            foreach (string validCard in ValidJCBs)
            {
                var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = validCard,
                        ExpiryDate = "022029"
                    }
                );

                var okResult = actionResult as OkObjectResult;

                // Assert
                Assert.Equal(200, okResult.StatusCode);
                ServerResponse resp = okResult.Value as ServerResponse;

                //expect is JCB
                Assert.True(CreditCardType.JCB.ToString() == ((CreditCardModel)resp.Result).Type, resp.RespDesc);
            }
        }

        [Fact]
        public async Task Test_Unknow()
        {
            //Test with 1111111111111111 with year 022029
            foreach (string validCard in UnknowCards)
            {
                var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = validCard,
                        ExpiryDate = "022029"
                    }
                );

                var okResult = actionResult as OkObjectResult;

                // Assert
                Assert.Equal(200, okResult.StatusCode);
                ServerResponse resp = okResult.Value as ServerResponse;

                //expect is Unknown card
                Assert.True(CreditCardType.Unknown.ToString() == ((CreditCardModel)resp.Result).Type, resp.RespDesc);
            }
        }

        [Fact]
        public async Task Test_InvalidDigits()
        {
            //Card Number (Numeric 15 or 16 digits)
            foreach (string validCard in InvalidDomainCards)
            {
                var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = validCard,
                        ExpiryDate = "022029"
                    }
                );

                var okResult = actionResult as OkObjectResult;

                // Assert
                Assert.Equal(200, okResult.StatusCode);
                ServerResponse resp = okResult.Value as ServerResponse;

                //expect is invalid number
                Assert.True(400 == resp.RespCode, resp.RespDesc);
            }
        }


        [Fact]
        public async Task Test_Invalid_Year_Format()
        {
            foreach (string validCard in ValidJCBs)
            {
                var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = validCard,
                        ExpiryDate = "werewr"
                    }
                );

                var okResult = actionResult as OkObjectResult;

                // Assert
                Assert.Equal(200, okResult.StatusCode);
                ServerResponse resp = okResult.Value as ServerResponse;

                //expect is invalid format
                Assert.True(400 == resp.RespCode, resp.RespDesc);
            }
        }

        [Fact]
        public async Task Test_Card_IS_EXPIRED()
        {
            foreach (string validCard in ValidJCBs)
            {
                var actionResult = await _cardValidationController.Validate(
                    new CardValidateRequest
                    {
                        CardNumber = validCard,
                        ExpiryDate = "022017"
                    }
                );

                var okResult = actionResult as OkObjectResult;

                // Assert
                Assert.Equal(200, okResult.StatusCode);
                ServerResponse resp = okResult.Value as ServerResponse;

                //expect is expired
                Assert.True(400 == resp.RespCode, resp.RespDesc);
            }
        }

        [Fact]
        public async Task Test_Visa_Starting_Not_LeapYear_Fail()
        {
            var actionResult = await _cardValidationController.Validate(
                new CardValidateRequest
                { CardNumber = "4397435601344857", ExpiryDate = "022021" }
                );

            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.Equal(200, okResult.StatusCode);
            ServerResponse resp = okResult.Value as ServerResponse;
            
            //expect is not a leap year
            Assert.True(400 == resp.RespCode, _expiryLeapYear + " is not a leap year");

        }

    }
}
