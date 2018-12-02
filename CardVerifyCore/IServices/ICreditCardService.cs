using CreditCardVerification.Data.Models;
using System.Threading.Tasks;

namespace CreditCardVerification.Interfaces.IServices
{
    public interface ICreditCardService
    {
        Task<CreditCardModel> VerifyCard(string id, string expiryDate);
    }
}
