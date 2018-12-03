using System.Linq;

namespace CreditCardVerification.Helpers
{
    internal static class StringHelper
    {
        public static string RemoveWhiteSpace(this string input)
        {
            if (input == null)
            {
                return null;
            }
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}