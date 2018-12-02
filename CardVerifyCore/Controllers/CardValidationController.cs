using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardVerifyCore.Models;
using CreditCardVerification.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CardVerifyCore.Controllers
{
    [Route("api/[controller]")]
    public class CardValidationController : Controller
    {

        private readonly ICreditCardService _iCreditCardService;

        public CardValidationController(ICreditCardService _iCreditCardService)
        {
            this._iCreditCardService = _iCreditCardService;
        }


        // GET api/cardvalidation/54717781544187/012018
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
                response.RespDesc = ex.Message;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            return Ok(response);
        }
    }
}
