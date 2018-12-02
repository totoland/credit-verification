using CreditCardVerification.Data.Entities;
using CreditCardVerification.Data.Models;
using System;
using System.Threading.Tasks;

namespace CreditCardVerification.Interfaces.IRepositories
{
    public interface ICreditCardRepository
    {
        Task<CreditCard> GetById(string id);
    }
}
