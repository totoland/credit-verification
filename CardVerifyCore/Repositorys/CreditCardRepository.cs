using System.Threading.Tasks;
using CreditCardVerification.Interfaces.IRepositories;
using CreditCardVerification.Data.Entities;
using CardVerifyCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardVerification.Interfaces.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly CreditCardContext _dbContext;
        public CreditCardRepository(CreditCardContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<CreditCard> GetById(string id)
        {
            var card = await _dbContext.CreditCard.FromSql($"VerifyCardNumber {id}").ToListAsync<CreditCard>();
            return card.Count == 0?null: card[0];
        }
    }
}
