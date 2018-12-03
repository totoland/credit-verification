using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardVerifyCore.Models;
using CreditCardVerification.Data.Models;
using CreditCardVerification.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardVerifyCore.Controllers
{
    [Route("api/creditcard")]
    public class CardValidationController : Controller
    {

        private readonly ICreditCardService _iCreditCardService;
        private readonly ILogger _logger;

        public CardValidationController(ICreditCardService _iCreditCardService,
            ILogger<CardValidationController> _logger)
        {
            this._iCreditCardService = _iCreditCardService;
            this._logger = _logger;
        }


        // GET api/cardvalidation/verify/54717781544187/012018
        [HttpGet("{cardnumber}/{expirydate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get(string cardnumber,string expirydate)
        {
            var response = new ServerResponse();

            try
            {
                var cardModel = await _iCreditCardService.VerifyCard(cardnumber, expirydate);

                if (cardModel == null)
                {
                    _logger.LogDebug("Credit card {0} does not exist on DB.", cardnumber);
                    response.RespDesc = "Credit card does not exist";
                }
                else
                {
                    response.Result = cardModel;
                    response.RespDesc = "";
                }

            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid input: ",ex.Message);
                response.RespDesc = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected exception ", ex.ToString());
                return StatusCode(500, "A problem happened while handling your request");
            }

            return Ok(response);
        }

        // GET post/cardvalidation/verify/
        [HttpPost("{request}", Name = "validate")]
        public async Task<IActionResult> Validate([FromBody]CreditCardModel request)
        {
            return await Get(request.CardNumber, request.ExpiryDate);
        }
    }
}
